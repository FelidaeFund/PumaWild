using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Manages flow of traffic along roads
/// 

public class TrafficManager : MonoBehaviour {

	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// NODES
	
	// external nodes created in the terrain
	public GameObject[] road1Nodes;
	public GameObject[] road2Nodes;
	public GameObject[] road3Nodes;

	// internal arrays includes extra nodes for node -1 and node n+1
	private NodeInfo[] nodeArray1Ascending;  
	private NodeInfo[] nodeArray2Ascending;
	private NodeInfo[] nodeArray3Ascending;
	private NodeInfo[] nodeArray1Descending;  
	private NodeInfo[] nodeArray2Descending;
	private NodeInfo[] nodeArray3Descending;
	
	// data structures for creating lane grids
	private class NodeInfo {
		public Vector3 position;
		public float segmentLength;
		public float segmentHeading;
		public float segmentPitch;
		public VirtualNodeInfo[] vNodes;   // lanes 1-3
	}
	private class VirtualNodeInfo {
		public Vector3 position;
		public float segmentLength;
		public float segmentPitch;
		public float segmentHeading;
		public float previousSegmentHeading;
		public float nextSegmentHeading;
	}
	
	// ROADS

	private class RoadInfo {
		public int lanesPerSide;
		public float laneSpeed1;
		public float laneSpeed2;
		public float laneSpeed3;
		public float followDistance1;
		public float followDistance2;
		public float followDistance3;
		public NodeInfo[] nodeArrayAscending;
		public NodeInfo[] nodeArrayDescending;
		public bool orientationIsX;
	}
	
	private RoadInfo[] roadArray;
	private bool road1OrientationIsX;  
	private bool road2OrientationIsX;  
	private bool road3OrientationIsX;  
	
	// VEHICLES
	
	private class VehicleInfo {
		public GameObject vehicle;
		public int lane;
		public float speed;
		public bool ascendingFlag;
		public NodeInfo[] nodeArray;
		public bool roadOrientationIsX;
		public int currentSegment;
		public float percentTravelled;
		public Vector3 segmentStartPos;
		public Vector3 segmentEndPos;
		public float segmentLength;
		public float segmentPitch;
		public float segmentHeading;
		public float previousSegmentHeading;
		public float nextSegmentHeading;
		public Vector3 terrainPos;
	}

	private List<VehicleInfo> vehicleList;
	
	public GameObject suvModel;
	
	// EXTERNAL MODULES
	private LevelManager levelManager;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start()
	{
		// connect to external modules
		levelManager = GetComponent<LevelManager>();
		
		// create NodeArrays with extra nodes for node -1 and node n+1
		nodeArray1Ascending = InitNodeArray(road1Nodes, true, false);
		nodeArray2Ascending = InitNodeArray(road2Nodes, true, true);
		nodeArray3Ascending = InitNodeArray(road3Nodes, true, true);
		nodeArray1Descending = InitNodeArray(road1Nodes, false, false);
		nodeArray2Descending = InitNodeArray(road2Nodes, false, true);
		nodeArray3Descending = InitNodeArray(road3Nodes, false, true);
	
		// create array of RoadInfo data structures
		roadArray = new RoadInfo[3];
		roadArray[0] = new RoadInfo();
		roadArray[1] = new RoadInfo();
		roadArray[2] = new RoadInfo();
		roadArray[0].nodeArrayAscending = nodeArray1Ascending;
		roadArray[1].nodeArrayAscending = nodeArray2Ascending;
		roadArray[2].nodeArrayAscending = nodeArray3Ascending;
		roadArray[0].nodeArrayDescending = nodeArray1Descending;
		roadArray[1].nodeArrayDescending = nodeArray2Descending;
		roadArray[2].nodeArrayDescending = nodeArray3Descending;
		roadArray[0].orientationIsX = false;
		roadArray[1].orientationIsX = true;
		roadArray[2].orientationIsX = true;
		
		// create empty vehicleList
		vehicleList = new List<VehicleInfo>();

		
		InitLevel(1);	// TEMP !!!!!
	}
	
	
	// initialize the NodeArrays with extra nodes for node -1 and node n+1
	
	private NodeInfo[] InitNodeArray(GameObject[] roadNodesAscending, bool ascendingFlag, bool orientationIsX)
	{
		GameObject[] roadNodes = null;
		Vector3 nodeOffsetVector = new Vector3(0f, 0f, 0f);;
		
		// access manually placed game objects (possibly with inversion)
		if (ascendingFlag == true) {
			// use existing roadNodes
			roadNodes = new GameObject[roadNodesAscending.Length];
			for (int i = 0; i < roadNodesAscending.Length; i++) {
				roadNodes[i] = roadNodesAscending[i];
			}
			nodeOffsetVector = (orientationIsX ? (new Vector3(-2000f, 0f, 0f)) : (new Vector3(0f, 0f, -2000f)));
		}
		else {
			// create descending version of roadNodes
			roadNodes = new GameObject[roadNodesAscending.Length];
			for (int i = 0; i < roadNodesAscending.Length; i++) {
				roadNodes[i] = roadNodesAscending[roadNodesAscending.Length-1-i];
			}
			nodeOffsetVector = (orientationIsX ? (new Vector3(2000f, 0f, 0f)) : (new Vector3(0f, 0f, 2000f)));
		}

		// create array
		int arraySize = roadNodes.Length+2;
		NodeInfo[] nodeArray = new NodeInfo[arraySize];

		// first node is node "-1" (projection of node "n")
		nodeArray[0] = new NodeInfo();
		nodeArray[0].position = roadNodes[roadNodes.Length-1].transform.position + nodeOffsetVector;

		// pull nodes from game object array
		for (int i = 1; i < roadNodes.Length+1; i++) {
			nodeArray[i] = new NodeInfo();
			nodeArray[i].position = roadNodes[i-1].transform.position;
		}

		// last node is node "n+1" (projection of node "0")
		nodeArray[arraySize-1] = new NodeInfo();
		nodeArray[arraySize-1].position = roadNodes[0].transform.position - nodeOffsetVector;

		// initialize node data
		float laneWidth = 12f;
		
		for (int i = 0; i < arraySize-1; i++) {
			// static info for each segment
			nodeArray[i].segmentLength = Vector3.Distance(nodeArray[i].position, nodeArray[i+1].position);					
			Vector2 segmentStartVector2 = new Vector2(nodeArray[i].position.x, nodeArray[i].position.z);
			Vector2 segmentEndVector2 = new Vector2(nodeArray[i+1].position.x, nodeArray[i+1].position.z);
			float segmentFlatDistance = Vector2.Distance(segmentStartVector2, segmentEndVector2);
			nodeArray[i].segmentHeading = levelManager.GetAngleFromOffset(nodeArray[i].position.x, nodeArray[i].position.z, nodeArray[i+1].position.x, nodeArray[i+1].position.z);
			nodeArray[i].segmentPitch = levelManager.GetAngleFromOffset(nodeArray[i+1].position.y, 0, nodeArray[i].position.y, segmentFlatDistance);
			// create vNodes for each segment
			nodeArray[i].vNodes = new VirtualNodeInfo[3];
			for (int lane = 0; lane < 3; lane++) {
				nodeArray[i].vNodes[lane] = new VirtualNodeInfo();
				// first determine composite heading based on previous segment and current segment
				float previousSegmentHeading = nodeArray[ (i == 0) ? (arraySize-3) : (i-1) ].segmentHeading;
				float currentSegmentHeading = nodeArray[i].segmentHeading;
				float vNodeOffsetDirection = InterpolateAngles(previousSegmentHeading, currentSegmentHeading, 0.5f) + 90f;
				// now create the vNode for this lane		
				float laneOffset = (laneWidth * 0.9f) + laneWidth * lane;
				float vNodeX = nodeArray[i].position.x + (Mathf.Sin(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				float vNodeZ = nodeArray[i].position.z + (Mathf.Cos(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				nodeArray[i].vNodes[lane].position = new Vector3(vNodeX, nodeArray[i].position.y, vNodeZ);
			}
		}

		// initialize vNode positions for last node
		{
			int i = arraySize-1;
			nodeArray[i].vNodes = new VirtualNodeInfo[3];
			for (int lane = 0; lane < 3; lane++) {
				nodeArray[i].vNodes[lane] = new VirtualNodeInfo();
				// first determine composite heading based on previous segment and current segment
				float previousSegmentHeading = nodeArray[ (i == 0) ? (arraySize-3) : (i-1) ].segmentHeading;
				float currentSegmentHeading = nodeArray[i].segmentHeading;
				float vNodeOffsetDirection = InterpolateAngles(previousSegmentHeading, currentSegmentHeading, 0.5f) + 90f;
				// now create the vNode for this lane		
				float laneOffset = (laneWidth * 0.9f) + laneWidth * lane;
				float vNodeX = nodeArray[i].position.x + (Mathf.Sin(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				float vNodeZ = nodeArray[i].position.z + (Mathf.Cos(vNodeOffsetDirection*Mathf.PI/180) * laneOffset);
				nodeArray[i].vNodes[lane].position = new Vector3(vNodeX, nodeArray[i].position.y, vNodeZ);
			}
		}

		// update static info for each vNode
		for (int i = 0; i < arraySize-1; i++) {
			for (int lane = 0; lane < 3; lane++) {
				Vector2 segmentStartVector2 = new Vector2(nodeArray[i].vNodes[lane].position.x, nodeArray[i].vNodes[lane].position.z);
				Vector2 segmentEndVector2 = new Vector2(nodeArray[i+1].vNodes[lane].position.x, nodeArray[i+1].vNodes[lane].position.z);
				float segmentFlatDistance = Vector2.Distance(segmentStartVector2, segmentEndVector2);
				nodeArray[i].vNodes[lane].segmentLength = Vector3.Distance(nodeArray[i].vNodes[lane].position, nodeArray[i+1].vNodes[lane].position);					
				nodeArray[i].vNodes[lane].segmentPitch = levelManager.GetAngleFromOffset(nodeArray[i+1].vNodes[lane].position.y, 0, nodeArray[i].vNodes[lane].position.y, segmentFlatDistance);
				nodeArray[i].vNodes[lane].segmentHeading = levelManager.GetAngleFromOffset(nodeArray[i].vNodes[lane].position.x, nodeArray[i].vNodes[lane].position.z, nodeArray[i+1].vNodes[lane].position.x, nodeArray[i+1].vNodes[lane].position.z);
			}
		}

		// update previousSegmentHeading and nextSegmentHeading for each vNode
		for (int i = 0; i < arraySize; i++) {
			for (int lane = 0; lane < 3; lane++) {
				int previousIndex = (i == 0) ? (arraySize-3) : (i-1);
				int nextIndex = (i == arraySize-1) ? (1) : (i+1);
				nodeArray[i].vNodes[lane].previousSegmentHeading = nodeArray[previousIndex].segmentHeading;
				nodeArray[i].vNodes[lane].nextSegmentHeading = nodeArray[nextIndex].segmentHeading;
			}
		}

		return nodeArray;
	}
	
	//===================================
	//===================================
	//		SET UP THE LEVEL
	//===================================
	//===================================

	public void InitLevel(int levelNum)
	{
		// remove any previously created vehicles
		for(int i=0; i<vehicleList.Count; i++)
			Destroy(vehicleList[i].vehicle);
		vehicleList.Clear();
		
		// configure the roads for the desired level
		if (levelNum < 1 || levelNum > 4) {
			Debug.Log("ERROR: TrafficManager told to initialize invalid level");
			return;
		}
		SelectRoadConfig(levelNum);
		
		// add the vehicles to each of the roads in each of the terrains

		for (int t=0; t<4; t++) {
			for (int r=0; r<3; r++) {
				int laneCount = 3;  //roadArray[r].lanesPerSide;
				for (int i = 0; i < laneCount; i++) {
					PopulateLane(r, i, true, levelManager.GetStartingTerrainX(t), levelManager.GetStartingTerrainZ(t));
					PopulateLane(r, i, false, levelManager.GetStartingTerrainX(t), levelManager.GetStartingTerrainZ(t));
				}
			}
		}
	}
	
	private void PopulateLane(int roadNum, int laneNum, bool ascendingFlag, float terrainX, float terrainZ)
	{
		float segmentPercent = 0f;
		float followDistance = (laneNum == 0) ? roadArray[roadNum].followDistance1 : ((laneNum == 1) ? roadArray[roadNum].followDistance2 : roadArray[roadNum].followDistance3);
		NodeInfo[] nodeArray = (ascendingFlag == true) ? roadArray[roadNum].nodeArrayAscending : roadArray[roadNum].nodeArrayDescending;

		int  i = 1;  // node 1 is effective node "0" because first node is node "-1"
		while (i < nodeArray.Length-1) {
			while (segmentPercent < 1f) {
				// add vehicles as long as the node has room
				VehicleInfo vehicleInfo = new VehicleInfo();
				vehicleInfo.terrainPos = new Vector3(terrainX, 0, terrainZ);
				vehicleInfo.vehicle = null;
				vehicleInfo.nodeArray = nodeArray;
				vehicleInfo.lane = laneNum;
				vehicleInfo.roadOrientationIsX = roadArray[roadNum].orientationIsX;	
				vehicleInfo.ascendingFlag = ascendingFlag;
				vehicleInfo.speed = (laneNum == 0) ? roadArray[roadNum].laneSpeed1 : ((laneNum == 1) ? roadArray[roadNum].laneSpeed2 : roadArray[roadNum].laneSpeed3);
				vehicleInfo.percentTravelled = segmentPercent;		
				vehicleInfo.currentSegment = i;
				vehicleInfo.segmentStartPos = nodeArray[i].vNodes[laneNum].position;
				vehicleInfo.segmentEndPos = nodeArray[i+1].vNodes[laneNum].position;
				vehicleInfo.segmentLength = nodeArray[i].vNodes[laneNum].segmentLength;
				vehicleInfo.segmentPitch = nodeArray[i].vNodes[laneNum].segmentPitch;
				vehicleInfo.segmentHeading = nodeArray[i].vNodes[laneNum].segmentHeading;
				vehicleInfo.previousSegmentHeading = nodeArray[i].vNodes[laneNum].previousSegmentHeading;
				vehicleInfo.nextSegmentHeading = nodeArray[i].vNodes[laneNum].nextSegmentHeading;
				vehicleList.Add(vehicleInfo);
				segmentPercent += followDistance / nodeArray[i].vNodes[laneNum].segmentLength;
			}
			while (segmentPercent >= 1f) {
				// increment to next node where vehicle needs to go
				float extraDistance = (segmentPercent-1f) * nodeArray[i].vNodes[laneNum].segmentLength;
				if (++i >= nodeArray.Length-1)
					break;
				segmentPercent = extraDistance / nodeArray[i].vNodes[laneNum].segmentLength;
			}
		}	
	}
	
	
	//===================================
	//===================================
	//		PROCESS THE VEHICLES
	//===================================
	//===================================

	void Update ()
	{
		for(int i=0; i<vehicleList.Count; i++) {
			VehicleInfo vehicleInfo = vehicleList[i];
			
			// add new distance to vehicle position
			float distanceTravelled = vehicleInfo.speed * Time.deltaTime;
			float segmentLengthRemaining = vehicleInfo.segmentLength * (1f - vehicleInfo.percentTravelled);
			if (distanceTravelled < segmentLengthRemaining) {
				// remain in current segment
				vehicleInfo.percentTravelled += distanceTravelled / vehicleInfo.segmentLength;
			}
			else {
				// progress to next segment
				vehicleInfo.currentSegment += 1;  // no need to check end condition; last segment wraps to first segment part way through
				vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].position;
				vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].vNodes[vehicleInfo.lane].position;
				vehicleInfo.segmentLength = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].segmentLength;
				vehicleInfo.segmentPitch = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].segmentPitch;
				vehicleInfo.segmentHeading = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].segmentHeading;
				vehicleInfo.previousSegmentHeading = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].previousSegmentHeading;
				vehicleInfo.nextSegmentHeading = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].nextSegmentHeading;
				vehicleInfo.percentTravelled = (distanceTravelled - segmentLengthRemaining) / vehicleInfo.segmentLength;						
			}
			
			// calculate new vehicle position
			Vector3 vehiclePos = vehicleInfo.terrainPos + Vector3.Lerp(vehicleInfo.segmentStartPos, vehicleInfo.segmentEndPos, vehicleInfo.percentTravelled);

			// if we've left our current terrain, loop back to start of node list for new terrain
			if (vehicleInfo.roadOrientationIsX == true) {
				if (vehicleInfo.ascendingFlag == true) {
					if (vehiclePos.x > vehicleInfo.terrainPos.x + 2000f) {
						vehicleInfo.currentSegment = 0;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].vNodes[vehicleInfo.lane].position;
						vehicleInfo.terrainPos += new Vector3(2000f, 0f, 0f);
					}
				}
				else {
					if (vehiclePos.x < vehicleInfo.terrainPos.x) {
						vehicleInfo.currentSegment = 0;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].vNodes[vehicleInfo.lane].position;
						vehicleInfo.terrainPos += new Vector3(-2000f, 0f, 0f);
					}
				}
			}
			else {
				if (vehicleInfo.ascendingFlag == true) {
					if (vehiclePos.z > vehicleInfo.terrainPos.z + 2000f) {
						vehicleInfo.currentSegment = 0;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].vNodes[vehicleInfo.lane].position;
						vehicleInfo.terrainPos += new Vector3(0f, 0f, 2000f);
					}
				}
				else {
					if (vehiclePos.z < vehicleInfo.terrainPos.z) {
						vehicleInfo.currentSegment = 0;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].vNodes[vehicleInfo.lane].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].vNodes[vehicleInfo.lane].position;
						vehicleInfo.terrainPos += new Vector3(0f, 0f, -2000f);
					}
				}
			}

			// if we're not over any terrain, adjust accordingly
			if (vehiclePos.x < levelManager.GetTerrainMinX()) {
				vehiclePos.x += 4000f;
				vehicleInfo.terrainPos.x += 4000f;
			}
			if (vehiclePos.z < levelManager.GetTerrainMinZ()) {
				vehiclePos.z += 4000f;
				vehicleInfo.terrainPos.z += 4000f;
			}
			if (vehiclePos.x > levelManager.GetTerrainMaxX()) {
				vehiclePos.x -= 4000f;
				vehicleInfo.terrainPos.x -= 4000f;
			}
			if (vehiclePos.z > levelManager.GetTerrainMaxZ()) {
				vehiclePos.z -= 4000f;
				vehicleInfo.terrainPos.z -= 4000f;
			}

			// no objects for vehicles far from puma

			float maxVisibleDistance = 500f;
			float distanceToPuma = Vector3.Distance(levelManager.pumaObj.transform.position, vehiclePos);			

			if (distanceToPuma < maxVisibleDistance && vehicleInfo.vehicle == null) {
				// close to puma; create object
				vehicleInfo.vehicle = Instantiate(suvModel, vehicleInfo.terrainPos, Quaternion.identity) as GameObject;
			}
			else if (distanceToPuma >= maxVisibleDistance && vehicleInfo.vehicle != null) {
				// far from puma; destroy object
				Destroy(vehicleInfo.vehicle);
				vehicleInfo.vehicle = null;
			}

			// if object, set location and rotation
			if (vehicleInfo.vehicle != null) {	
				vehicleInfo.vehicle.transform.position = vehiclePos;
				float heading = vehicleInfo.segmentHeading;
				if (vehicleInfo.percentTravelled < 0.05f) {
					float scaleFactor = vehicleInfo.percentTravelled / 0.05f;
					scaleFactor = 1f - ((scaleFactor-1f) * (scaleFactor-1f));
					heading = InterpolateAngles(vehicleInfo.segmentHeading, vehicleInfo.previousSegmentHeading, 0.5f + (0.5f*scaleFactor));
				}
				else if (vehicleInfo.percentTravelled > 0.95f) {
					float scaleFactor = (vehicleInfo.percentTravelled - 0.95f) / 0.05f;
					scaleFactor = scaleFactor * scaleFactor;
					heading = InterpolateAngles(vehicleInfo.segmentHeading, vehicleInfo.nextSegmentHeading, 1f - (0.5f*scaleFactor));
				}
				vehicleInfo.vehicle.transform.rotation = Quaternion.Euler(vehicleInfo.segmentPitch, heading, 0);
			}
		}
	}
	
	
	//===================================
	//===================================
	//		SELECT ROAD CONFIG
	//===================================
	//===================================

	private void SelectRoadConfig(int levelNum)
	{
		switch (levelNum) {

		case 1:  // level 2
			roadArray[0].lanesPerSide = 3;
			roadArray[0].laneSpeed1 = 70;
			roadArray[0].laneSpeed2 = 90;
			roadArray[0].laneSpeed3 = 110;
			roadArray[0].followDistance1 = 40;
			roadArray[0].followDistance2 = 50;
			roadArray[0].followDistance3 = 60;
			////////////
			roadArray[1].lanesPerSide = 3;
			roadArray[1].laneSpeed1 = 70;
			roadArray[1].laneSpeed2 = 90;
			roadArray[1].laneSpeed3 = 110;
			roadArray[1].followDistance1 = 40;
			roadArray[1].followDistance2 = 50;
			roadArray[1].followDistance3 = 60;
			////////////
			roadArray[2].lanesPerSide = 3;
			roadArray[2].laneSpeed1 = 70;
			roadArray[2].laneSpeed2 = 90;
			roadArray[2].laneSpeed3 = 110;
			roadArray[2].followDistance1 = 40;
			roadArray[2].followDistance2 = 50;
			roadArray[2].followDistance3 = 60;
			break;
		
		case 2:  // level 3
			roadArray[0].lanesPerSide = 3;
			roadArray[0].laneSpeed1 = 70;
			roadArray[0].laneSpeed2 = 90;
			roadArray[0].laneSpeed3 = 110;
			roadArray[0].followDistance1 = 150;
			roadArray[0].followDistance2 = 200;
			roadArray[0].followDistance3 = 300;
			////////////
			roadArray[1].lanesPerSide = 3;
			roadArray[1].laneSpeed1 = 70;
			roadArray[1].laneSpeed2 = 90;
			roadArray[1].laneSpeed3 = 110;
			roadArray[1].followDistance1 = 150;
			roadArray[1].followDistance2 = 200;
			roadArray[1].followDistance3 = 300;
			////////////
			roadArray[2].lanesPerSide = 3;
			roadArray[2].laneSpeed1 = 70;
			roadArray[2].laneSpeed2 = 90;
			roadArray[2].laneSpeed3 = 110;
			roadArray[2].followDistance1 = 150;
			roadArray[2].followDistance2 = 200;
			roadArray[2].followDistance3 = 300;
			break;
		
		case 3:  // level 4
			roadArray[0].lanesPerSide = 3;
			roadArray[0].laneSpeed1 = 70;
			roadArray[0].laneSpeed2 = 90;
			roadArray[0].laneSpeed3 = 110;
			roadArray[0].followDistance1 = 150;
			roadArray[0].followDistance2 = 200;
			roadArray[0].followDistance3 = 300;
			////////////
			roadArray[1].lanesPerSide = 3;
			roadArray[1].laneSpeed1 = 70;
			roadArray[1].laneSpeed2 = 90;
			roadArray[1].laneSpeed3 = 110;
			roadArray[1].followDistance1 = 150;
			roadArray[1].followDistance2 = 200;
			roadArray[1].followDistance3 = 300;
			////////////
			roadArray[2].lanesPerSide = 3;
			roadArray[2].laneSpeed1 = 70;
			roadArray[2].laneSpeed2 = 90;
			roadArray[2].laneSpeed3 = 110;
			roadArray[2].followDistance1 = 150;
			roadArray[2].followDistance2 = 200;
			roadArray[2].followDistance3 = 300;
			break;
		
		case 4:  // level 5
			roadArray[0].lanesPerSide = 3;
			roadArray[0].laneSpeed1 = 70;
			roadArray[0].laneSpeed2 = 90;
			roadArray[0].laneSpeed3 = 110;
			roadArray[0].followDistance1 = 150;
			roadArray[0].followDistance2 = 200;
			roadArray[0].followDistance3 = 300;
			////////////
			roadArray[1].lanesPerSide = 3;
			roadArray[1].laneSpeed1 = 70;
			roadArray[1].laneSpeed2 = 90;
			roadArray[1].laneSpeed3 = 110;
			roadArray[1].followDistance1 = 150;
			roadArray[1].followDistance2 = 200;
			roadArray[1].followDistance3 = 300;
			////////////
			roadArray[2].lanesPerSide = 3;
			roadArray[2].laneSpeed1 = 70;
			roadArray[2].laneSpeed2 = 90;
			roadArray[2].laneSpeed3 = 110;
			roadArray[2].followDistance1 = 150;
			roadArray[2].followDistance2 = 200;
			roadArray[2].followDistance3 = 300;
			break;
		}
	}

	//===================================
	//===================================
	//		UTILS
	//===================================
	//===================================

	private float InterpolateAngles(float angle1, float angle2, float angle1Percent)
	{
		float interpolatedAngle = 0f;

		if (angle1 < 0f)
			angle1 += 360f;
		if (angle2 < 0f)
			angle2 += 360f;
	
		if (angle2 > angle1) {
			if (angle2 - angle1 < 180f) {
				interpolatedAngle = angle2 - ((angle2 - angle1) * angle1Percent);
			}
			else {
				angle1 += 360f;
				interpolatedAngle = angle1 - ((angle1 - angle2) * (1f-angle1Percent));
			}
		}
		else {
			if (angle1 - angle2 < 180f) {
				interpolatedAngle = angle1 - ((angle1 - angle2) * (1f-angle1Percent));
			}
			else {
				angle2 += 360f;
				interpolatedAngle = angle2 - ((angle2 - angle1) * angle1Percent);
			}
		}

		return interpolatedAngle;
	}



/*





	
	
	
	
	
	// Segment motion
	private GameObject virtualStartNode;
	private GameObject virtualTargetNode;
	private float journeyLength;
	private float distanceCovered;
    private float percentTravelled;
    private float startTime;

    // Whole path (from laneX-node0 to laneX-nodeN)
    private int startNode;
    private int endNode;
    private int currentNode;

    // Car configurations
    private float speed;
	private int currentLane;
	private float virtualLaneAdjustment;
	private string displacementAlongAxis;

	
	
	
	

	private Transform newCarFirstNode;
	private Transform newCarSecondNode;
	private int newCarStartNode;
	private int newCarEndNode;
	

	private class RoadInfo {
		public int numLanes;
		public float roadWidth;
		public float averageCarSpeed;
		public float averageCarDistance;
		public float nextCarCreationTime;
		public string displacementAlongAxis;
		public GameObject[] nodeArray;
		public GameObject[] carList;
	}
	
	private RoadInfo[] roadArray;
	
	public GameObject suvModel;

	
	void Start()
	{
		roadArray = new RoadInfo[3];

		roadArray[0].numLanes = 2;
		roadArray[0].roadWidth = 20f;
		roadArray[0].averageSpeed = 60;
		roadArray[0].averageCarsPerMinute = 30;
		roadArray[0].nextCarCreationTime = Time.time;
		roadArray[0].displacementAlongAxis = "y";
		roadArray[0].nodeList = road1Nodes;

		roadArray[1].numLanes = 2;
		roadArray[1].roadWidth = 20f;
		roadArray[1].averageSpeed = 60;
		roadArray[1].averageCarsPerMinute = 30;
		roadArray[1].nextCarCreationTime = Time.time;
		roadArray[1].displacementAlongAxis = "y";
		roadArray[1].nodeList = road2Nodes;

		roadArray[2].numLanes = 2;
		roadArray[2].roadWidth = 20f;
		roadArray[2].averageSpeed = 60;
		roadArray[2].averageCarsPerMinute = 30;
		roadArray[2].nextCarCreationTime = Time.time;
		roadArray[2].displacementAlongAxis = "y";
		roadArray[2].nodeList = road3Nodes;
	}
	
	
	// Update is called once per frame
	void Update () {
		// Road data
		RoadInfo roadInfo;
		RoadInfo partnerInfo;
		// Amount of active cars in the road
		int amountOfCars;
		int numberOfNodes;
		int partnerLength;
		// Points to the game object
		GameObject car;
		// Points to the gamObject's component "CarInfo", which stores data about the car.
		CarInfo carInfo;
		
		//iterates through all the roads we have(12).
//		for(int i=0; i<roadsList.Length; i++)
//		{	
			//Debug.Log(roadsList[i]);
			//Gets the "RoadInfo" component within the specific road we are looking at
			//roadInfo = roadsList[i].GetComponent<RoadInfo>();
			//partnerInfo = roadInfo.partnerRoad.GetComponent<RoadInfo>();

			roadInfo = firstRoadInfo;
			partnerInfo = firstRoadInfo;


			// Checks whether this road is enabled or not
			//if (roadInfo.enabled == false) break;

			numberOfNodes = road1Nodes.Length;
			partnerLength = road1Nodes.Length;
			
			// See if we need to add a car to this road
			if (Time.time >= roadInfo.nextCarCreationTime) {
				GameObject newCar = createNewCar(roadInfo, numberOfNodes, partnerInfo, partnerLength);
				// It will return NULL if the car should be created in the partner road,
				// We don't want the road to be responsible for generating cars coming from the other direction
				if(newCar != null)
				{
					roadInfo.carList.Add(newCar);
					roadInfo.nextCarCreationTime = Time.time + (60/roadInfo.averageCarsPerMinute)+Random.Range(0, 5);
				}
			}
			
			// Iterate through each car on this road		
			amountOfCars = roadInfo.carList.Count;
			for(int j=0; j<amountOfCars; j++)
			{
				// Points to the car GameObject
				car = roadInfo.carList[j];
				// Points to the carInfo (car's meta data).
				carInfo = car.GetComponent<CarInfo>();
				
				// Segment percent travelled by the car
				float percentTravelled = carInfo.getPercentTravelled();
				// if percentTravelled > 100%
				if(percentTravelled > 1.0f)
				{
					//Checks whether the next node the car is heading to exists or not. (inside the same road)
					int nextNode = carInfo.getNextNode();
					int currentNode = carInfo.getCurrentNode();
					// If car is has not reached any of the two extremes of the road
					if(nextNode < numberOfNodes && nextNode != -1)
					{
						// Updates car path: sets segment from current position to next node's position
						carInfo.updatePath(roadInfo.nodes[currentNode].position, roadInfo.nodes[nextNode].position); //update segment's start and end
						// Rotates Y-axis to point the car towards its target
						// place-holder for rotation algorithm, that will be handled outside of this if
						carInfo.lookAtNextNode();
					}

					// Car has reached either node0 or nodeN 
					else
					{
						// We are going down (N to 0)
						// And the car is at the border 0, now.
						if(nextNode == -1)
						{
							// Computes distance between this border and the begining of next road
							float nodesDistance = Vector3.Distance(car.transform.position, partnerInfo.nodes[partnerLength-1].position);
							// If there is road to keep going ahead, we change the road the car is at
							if(nodesDistance<1000f)
							{	
								Transform newVirtualTargetNode = partnerInfo.nodes[partnerLength-1];
								int newStartNode = partnerLength-1;
								int newEndNode = 0;
								carInfo.changeRoad(car.transform.position, newVirtualTargetNode.position, newStartNode, newEndNode);
								// Remove car from this road
								roadInfo.carList.RemoveAt(j);
								amountOfCars -= 1;
								// Add to the partner road
								partnerInfo.carList.Add(car);
							}
							// We destroy the car, otherwise
							else
							{
								int carId = j;
								destroyCar(carInfo, roadInfo, carId, car);
								break;
							}
						}

						// We are going up (0 to N)
						// And the car is at the border N, now.
						else
						{
							// Computes distance between this border and the begining of next road
							float nodesDistance = Vector3.Distance(car.transform.position, partnerInfo.nodes[0].position);
							// If there is road to keep going ahead, we change the road the car is at
							if(nodesDistance<50f)
							{
								Transform newVirtualTargetNode = partnerInfo.nodes[0];
								int newStartNode = 0;
								int newEndNode = partnerLength-1;
								carInfo.changeRoad(car.transform.position, newVirtualTargetNode.position, newStartNode, newEndNode);
								// Remove car from this road
								roadInfo.carList.RemoveAt(j);
								amountOfCars -= 1;
								// Add to the partner road
								partnerInfo.carList.Add(car);
							}
							// We destroy the car, otherwise
							else
							{
								int carId = j;
								destroyCar(carInfo, roadInfo, carId, car);
								break;
							}
						}
					}

				}
				
													
				// set position based on startNode, endNode and percentTravelled
				carInfo.moveCar();

				// set rotation based on different cases
				
					// first half of first segment of first road

					
					// last half of last segment of first road
					
					
					// first half of first segment of second road
					
					
					// last half of last segment of second road				

					
					// normal case (none of the above)
					
			}
//		}
	}

	private GameObject createNewCar(RoadInfo roadInfo, int numberOfNodes, RoadInfo partnerInfo, int partnerLength)
	{
		bool carCreated = false;
		// Getting information necessary to set car journey
		float roadWidth = roadInfo.roadWidth;
		int roadLanes = roadInfo.numLanes;
		int newCarLane = Random.Range(1, roadLanes+1);

		// Gets max speed alowed by the lane this car will move at
		float carMaxSpeed = roadInfo.laneSpeed[newCarLane-1];

		string displacementAlongAxis = roadInfo.displacementAlongAxis;

		//=============================================================
		// Decides whether car is going "up" or "down" the road
		// First half of the lanes go from node 0 to last node. (up)
		// Second half of the lanes go from last node to 0. (down)
		//=============================================================

		// Decides which of the two roads is the begining of the big road (both together)
		bool isThisFirstRoad = false;
		float nodesDistance = Vector3.Distance(roadInfo.nodes[0].position, partnerInfo.nodes[partnerLength-1].position);
		if(nodesDistance>100f) isThisFirstRoad = true;
		// If car is in the first half of the lanes (going up, from 0 to N)
		if(newCarLane <= roadLanes/2)
		{
			// If this road is the first, the car will be generated at node 0 of this road
			if (isThisFirstRoad == true)
			{
				newCarFirstNode = roadInfo.nodes[0];
				newCarSecondNode = roadInfo.nodes[1];
				newCarStartNode = 0;
				newCarEndNode = numberOfNodes-1;
				carCreated = true;
			}
			// We ignore if the car should come from the other direction (partner road)
		}
		// If car is in the first half of the lanes (going down, from N to 0)
		else
		{
			// If this road is the second, the car will be generated at the node N of this road
			if (isThisFirstRoad == false)
			{
				newCarFirstNode = roadInfo.nodes[partnerLength-1];
				newCarSecondNode = roadInfo.nodes[partnerLength-2];
				newCarStartNode = partnerLength-1;
				newCarEndNode = 0;
				carCreated = true;
			}
			// We ignore if the car should come from the other direction (partner road)
		}

		if(carCreated==true)
		{
			GameObject newCar = Instantiate(suvModel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			CarInfo newCarInfo = newCar.GetComponent<CarInfo>();

			// Sets the configuration above to the new car
			newCarInfo.setJourney(newCarFirstNode, newCarSecondNode, newCarStartNode, newCarEndNode, roadWidth, roadLanes, newCarLane, carMaxSpeed, displacementAlongAxis);
			
			// returns a pointer to the new car GameObject
			return newCar;
		}
		return null;
	}

	private void destroyCar(CarInfo carInfo, RoadInfo roadInfo, int carId, GameObject car)
	{
		carInfo.stopCar();
		carInfo.destroyNodes();
		roadInfo.carList.RemoveAt(carId);
		Destroy(car);
	}
	
	
	
	
	
	
	
	
	// car stuff
	
	
	
	
	
	// Segment motion
	private GameObject virtualStartNode;
	private GameObject virtualTargetNode;
	private float journeyLength;
	private float distanceCovered;
    private float percentTravelled;
    private float startTime;

    // Whole path (from laneX-node0 to laneX-nodeN)
    private int startNode;
    private int endNode;
    private int currentNode;

    // Car configurations
    private float speed;
	private int currentLane;
	private float virtualLaneAdjustment;
	private string displacementAlongAxis;

	public void Start()
	{
		//virtualStartNode = new GameObject();
		//virtualTargetNode = new GameObject();
	}

	//=======================
	// Getters and Setters
	//=======================

	// Setters

	// Sets the start time for the current segment
	public void setStartTime(float time)
	{
		startTime = time;
	}
	// Set percent travelled to zero (reset)
	public void resetPercentTravelled()
	{
		percentTravelled = 0.0f;
	}

	// Getters

	// Returns the current percentTravalled
	public float getPercentTravelled()
	{
		computePercentTravelled(Time.time);
		return percentTravelled;
	}
	// Returns the Vector3 of the virtual start node
	public Vector3 getVirtualStartNode()
	{
		return virtualStartNode.transform.position;
	}
	// Returns the Vector3 of the virtual target node
	public Vector3 getVirtualTargetNode()
	{
		return virtualTargetNode.transform.position;
	}
	// Returns the lane the car is positioned at
	public int getCurrentLane()
	{
		return currentLane;
	}

	public int getCurrentNode()
	{
		return currentNode;
	}


	//=======================
	// Methods
	//=======================
	//THIS SHOULD REMOVE THE Start() function
	public void setJourney(Transform newCarFirstNode, Transform newCarSecondNode, int newCarStartNode, int newCarEndNode, float roadWidth, int roadLanes, int newCarLane, float carMaxSpeed, string displacementAlongAxis)
	{
		// Path configuration
	    startNode = newCarStartNode;
	    endNode = newCarEndNode;
	    currentNode = startNode;
	    // Lane this car will drive at
	    currentLane = newCarLane;
	    // Car Max speed
	    speed = carMaxSpeed;
	    // Computes the adjustment for the virtual node, based on road width and number of lanes
	    computeLaneAdjustment(roadWidth, roadLanes);
	    // Sets the initial virtualStartNode and virtualTargetNode, based on first and second actual node positions
	    virtualStartNode = new GameObject();
	    virtualTargetNode = new GameObject();

	    this.displacementAlongAxis = displacementAlongAxis;
	    // Find out if the road segment goes in the X or Z axis
		if(displacementAlongAxis == "z")
		{
			virtualStartNode.transform.position = new Vector3(newCarFirstNode.position.x+virtualLaneAdjustment, newCarFirstNode.position.y, newCarFirstNode.position.z);
			virtualTargetNode.transform.position = new Vector3(newCarSecondNode.position.x+virtualLaneAdjustment, newCarSecondNode.position.y, newCarSecondNode.position.z);
		}
		else
		{
			virtualStartNode.transform.position = new Vector3(newCarFirstNode.position.x, newCarFirstNode.position.y, newCarFirstNode.position.z+virtualLaneAdjustment);
			virtualTargetNode.transform.position = new Vector3(newCarSecondNode.position.x, newCarSecondNode.position.y, newCarSecondNode.position.z+virtualLaneAdjustment);
		}
		transform.position = virtualStartNode.transform.position;
		// Sets segment journey information
		distanceCovered = 0.0f;
		percentTravelled = 0.0f;
		// Computes the length of the first segment
		journeyLength = Vector3.Distance(virtualStartNode.transform.position, virtualTargetNode.transform.position);
		// Do I really need it here? Should I put it here? I guess I should trigger sth lik "car.Run()" from the traffic manager
		//setStartTime(Time.time);
	
	}

	// Computes the percent travelled
	private void computePercentTravelled(float currentTime)
	{
		distanceCovered = (currentTime - startTime) * speed;
		percentTravelled = distanceCovered / journeyLength;
	}

	// Computes the adjustment on cars x-asis for the virtual nodes (to virtualize lanes)
	private void computeLaneAdjustment(float roadWidth, int numLanes)
	{
		float laneWidth = roadWidth/numLanes;
		virtualLaneAdjustment = ((numLanes/2 - currentLane)*laneWidth) + laneWidth/2;
	}

	public void updatePath(Vector3 from, Vector3 to)
	{
		// Find out if the road segment goes in the X or Z axis
		if(displacementAlongAxis == "z")
		{
			//Updates virtualTargetNode based on lane number and road width
			virtualStartNode.transform.position = new Vector3(from.x+virtualLaneAdjustment, from.y, from.z);
			virtualTargetNode.transform.position = new Vector3(to.x+virtualLaneAdjustment, to.y, to.z);
		}
		else
		{
			//Updates virtualTargetNode based on lane number and road width
			virtualStartNode.transform.position = new Vector3(from.x, from.y, from.z+virtualLaneAdjustment);
			virtualTargetNode.transform.position = new Vector3(to.x, to.y, to.z+virtualLaneAdjustment);
		}
		// Set the car in the next node (begining of the segment)
		currentNode = getNextNode();
		distanceCovered = 0.0f;
		percentTravelled = 0.0f;
		journeyLength = Vector3.Distance(virtualStartNode.transform.position, virtualTargetNode.transform.position);
		setStartTime(Time.time);
	}

	// Updates path configuration to new road
	public void changeRoad(Vector3 from, Vector3 to, int newStartNode, int newEndNode)
	{
		// Set start of the segment as current position
		virtualStartNode.transform.position = new Vector3(from.x, from.y, from.z);
		// Set target of this segment as the first or last node of next road, but with lane virtualization
		// Find out if the road segment goes in the X or Z axis
		if(displacementAlongAxis == "z")
		{
			virtualTargetNode.transform.position = new Vector3(to.x+virtualLaneAdjustment, to.y, to.z);
		}
		else
		{
			virtualTargetNode.transform.position = new Vector3(to.x, to.y, to.z+virtualLaneAdjustment);
		}
		// Updates configuration of the whole path (from "road_nodes[0]" to "road_nodes[last]")
		startNode = newStartNode;
		endNode = newEndNode;
		currentNode = newStartNode;
		// Set configurarations for the journey in this segment
		distanceCovered = 0.0f;
		percentTravelled = 0.0f;
		journeyLength = Vector3.Distance(virtualStartNode.transform.position, virtualTargetNode.transform.position);
		setStartTime(Time.time);
	}

	public int getNextNode()
	{
		if(endNode>startNode) return currentNode+1;
		return currentNode-1;
	}

	public void lookAtNextNode()
	{
		transform.LookAt(new Vector3(virtualTargetNode.transform.position.x, transform.position.y, virtualTargetNode.transform.position.z));
	}

	public void stopCar()
	{
		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
	}

	public void moveCar()
	{
		transform.position = Vector3.Lerp(virtualStartNode.transform.position, virtualTargetNode.transform.position, percentTravelled);
	}

	public void destroyNodes()
	{
		Destroy(virtualStartNode);
		Destroy(virtualTargetNode);
	}


}

	
*/
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
