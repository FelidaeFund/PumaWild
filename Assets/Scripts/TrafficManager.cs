﻿using UnityEngine;
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
	private NodeInfo[] nodeArray1;  
	private NodeInfo[] nodeArray2;
	private NodeInfo[] nodeArray3;
	private bool road1OrientationIsX;  
	private bool road2OrientationIsX;  
	private bool road3OrientationIsX;  
	
	// data structures for creating lane grids
	private class NodeInfo {
		public Vector3 position;
		public Vector3[] vNodesAscending;	// lanes 1-3 ascending
		public Vector3[] vNodesDescending;	// lanes 1-3 descending
	}
	private class VirtualNodeInfo {
		public Vector3 position;
		public float segementHeading;
		public float segementPitch;
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
		public NodeInfo[] nodeArray;
		public bool orientationIsX;
	}
	
	private RoadInfo[] roadArray;
	
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
		public float lastUpdateTime;
		public Vector3 segmentStartPos;
		public Vector3 segmentEndPos;
		public float segmentHeading;
		public float segmentPitch;
		public float segmentLength;
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

		nodeArray1 = new NodeInfo[road1Nodes.Length+2];
		nodeArray1[0] = new NodeInfo();
		nodeArray1[0].position = road1Nodes[road1Nodes.Length-1].transform.position + new Vector3(0f, 0f, -2000f);
		for (int i = 1; i < road1Nodes.Length+1; i++) {
			nodeArray1[i] = new NodeInfo();
			nodeArray1[i].position = road1Nodes[i-1].transform.position;
		}
		nodeArray1[road1Nodes.Length+1] = new NodeInfo();
		nodeArray1[road1Nodes.Length+1].position = road1Nodes[0].transform.position + new Vector3(0f, 0f, 2000f);

		nodeArray2 = new NodeInfo[road2Nodes.Length+2];
		nodeArray2[0] = new NodeInfo();
		nodeArray2[0].position = road2Nodes[road2Nodes.Length-1].transform.position + new Vector3(-2000f, 0f, 0f);
		for (int i = 1; i < road2Nodes.Length+1; i++) {
			nodeArray2[i] = new NodeInfo();
			nodeArray2[i].position = road2Nodes[i-1].transform.position;
		}
		nodeArray2[road2Nodes.Length+1] = new NodeInfo();
		nodeArray2[road2Nodes.Length+1].position = road2Nodes[0].transform.position + new Vector3(2000f, 0f, 0f);
	
		nodeArray3 = new NodeInfo[road3Nodes.Length+2];
		nodeArray3[0] = new NodeInfo();
		nodeArray3[0].position = road3Nodes[road3Nodes.Length-1].transform.position + new Vector3(-2000f, 0f, 0f);
		for (int i = 1; i < road3Nodes.Length+1; i++) {
			nodeArray3[i] = new NodeInfo();
			nodeArray3[i].position = road3Nodes[i-1].transform.position;
		}
		nodeArray3[road3Nodes.Length+1] = new NodeInfo();
		nodeArray3[road3Nodes.Length+1].position = road3Nodes[0].transform.position + new Vector3(2000f, 0f, 0f);

		// create array of RoadInfo data structures
		roadArray = new RoadInfo[3];
		roadArray[0] = new RoadInfo();
		roadArray[1] = new RoadInfo();
		roadArray[2] = new RoadInfo();
		roadArray[0].nodeArray = nodeArray1;
		roadArray[1].nodeArray = nodeArray2;
		roadArray[2].nodeArray = nodeArray3;
		roadArray[0].orientationIsX = false;
		roadArray[1].orientationIsX = true;
		roadArray[2].orientationIsX = true;
		
		// create empty vehicleList
		vehicleList = new List<VehicleInfo>();

		
		InitLevel(1);	// TEMP !!!!!
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

		for (int t=0; t<1; t++) {
		
			float terrainX = 0f;
			float terrainZ = 0f;
		
			switch (t) {
			case 0:
				terrainX = -2000f;
				terrainZ =  0f;
				break;
			case 1:
				terrainX = 0f;
				terrainZ = 0f;
				break;
			case 2:
				terrainX = -2000f;
				terrainZ = -2000f;
				break;
			case 3:
				terrainX =  0f;
				terrainZ = -2000f;
				break;
			}
		
			for (int r=0; r<3; r++) {
				for(int i=1; i < roadArray[r].nodeArray.Length-1; i++) {	
					for (int j=0; j<10; j++) {
						VehicleInfo vehicleInfo = new VehicleInfo();
						vehicleInfo.terrainPos = new Vector3(terrainX, 0, terrainZ);
						vehicleInfo.vehicle = null;
						
						vehicleInfo.nodeArray = roadArray[r].nodeArray;
						vehicleInfo.roadOrientationIsX = roadArray[r].orientationIsX;	
						vehicleInfo.currentSegment = i;
						vehicleInfo.ascendingFlag = true;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].position;
						vehicleInfo.segmentLength = Vector3.Distance(vehicleInfo.segmentStartPos, vehicleInfo.segmentEndPos);
							
						Vector2 segmentStartVector2 = new Vector2(vehicleInfo.segmentStartPos.x, vehicleInfo.segmentStartPos.z);
						Vector2 segmentEndVector2 = new Vector2(vehicleInfo.segmentEndPos.x, vehicleInfo.segmentEndPos.z);
						float segmentFlatDistance = Vector2.Distance(segmentStartVector2, segmentEndVector2);
						vehicleInfo.segmentHeading = levelManager.GetAngleFromOffset(vehicleInfo.segmentStartPos.x, vehicleInfo.segmentStartPos.z, vehicleInfo.segmentEndPos.x, vehicleInfo.segmentEndPos.z);
						vehicleInfo.segmentPitch = levelManager.GetAngleFromOffset(vehicleInfo.segmentEndPos.y, 0, vehicleInfo.segmentStartPos.y, segmentFlatDistance);
						vehicleInfo.lastUpdateTime = Time.time;
						vehicleInfo.percentTravelled = 0.1f * j;		
						vehicleInfo.speed = roadArray[r].laneSpeed1;
						
						vehicleList.Add(vehicleInfo);
					}
				}
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
			float distanceTravelled = vehicleInfo.speed * Time.deltaTime;
			float segmentLengthRemaining = vehicleInfo.segmentLength * (1f - vehicleInfo.percentTravelled);
			
			if (distanceTravelled < segmentLengthRemaining) {
				// remain in current segment
				vehicleInfo.percentTravelled += distanceTravelled / vehicleInfo.segmentLength;
			}
			else {
				// progress to next segment
				vehicleInfo.currentSegment += 1;  // no need to check end condition; last segment wraps to first segment part way through
				
				vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].position;
				vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].position;
				vehicleInfo.segmentLength = Vector3.Distance(vehicleInfo.segmentStartPos, vehicleInfo.segmentEndPos);
					
				Vector2 segmentStartVector2 = new Vector2(vehicleInfo.segmentStartPos.x, vehicleInfo.segmentStartPos.z);
				Vector2 segmentEndVector2 = new Vector2(vehicleInfo.segmentEndPos.x, vehicleInfo.segmentEndPos.z);
				float segmentFlatDistance = Vector2.Distance(segmentStartVector2, segmentEndVector2);
				vehicleInfo.segmentHeading = levelManager.GetAngleFromOffset(vehicleInfo.segmentStartPos.x, vehicleInfo.segmentStartPos.z, vehicleInfo.segmentEndPos.x, vehicleInfo.segmentEndPos.z);
				vehicleInfo.segmentPitch = levelManager.GetAngleFromOffset(vehicleInfo.segmentEndPos.y, 0, vehicleInfo.segmentStartPos.y, segmentFlatDistance);
				vehicleInfo.percentTravelled = (distanceTravelled - segmentLengthRemaining) / vehicleInfo.segmentLength;						
			}
			
			// calculate vehicle position
			Vector3 vehiclePos = vehicleInfo.terrainPos + Vector3.Lerp(vehicleInfo.segmentStartPos, vehicleInfo.segmentEndPos, vehicleInfo.percentTravelled);
			// adjust positioning logic if we've left our current terrain
			if (vehicleInfo.roadOrientationIsX == true) {
				if (vehicleInfo.ascendingFlag == true) {
					if (vehiclePos.x > vehicleInfo.terrainPos.x + 2000f) {
						vehicleInfo.currentSegment = 0;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].position;
						vehicleInfo.terrainPos += new Vector3(2000f, 0f, 0f);
					}
				}
				else {
					if (vehiclePos.x < vehicleInfo.terrainPos.x) {
					}
				}
			}
			else {
				if (vehicleInfo.ascendingFlag == true) {
					if (vehiclePos.z > vehicleInfo.terrainPos.z + 2000f) {
						vehicleInfo.currentSegment = 0;
						vehicleInfo.segmentStartPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment].position;
						vehicleInfo.segmentEndPos = vehicleInfo.nodeArray[vehicleInfo.currentSegment+1].position;
						vehicleInfo.terrainPos += new Vector3(0f, 0f, 2000f);
					}
				}
				else {
					if (vehiclePos.z < vehicleInfo.terrainPos.z) {
					}
				}
			}
			// recalculate vehicle position in case it was changed
			vehiclePos = vehicleInfo.terrainPos + Vector3.Lerp(vehicleInfo.segmentStartPos, vehicleInfo.segmentEndPos, vehicleInfo.percentTravelled);

			// no objects for vehicles far from puma
			float distanceToPuma = Vector3.Distance(levelManager.pumaObj.transform.position, vehiclePos);			
			if (distanceToPuma < 50000f && vehicleInfo.vehicle == null) {
				// close to puma; create object
				vehicleInfo.vehicle = Instantiate(suvModel, vehicleInfo.terrainPos, Quaternion.identity) as GameObject;
			}
			else if (distanceToPuma >= 50000f && vehicleInfo.vehicle != null) {
				// far from puma; destroy object
				Destroy(vehicleInfo.vehicle);
				vehicleInfo.vehicle = null;
			}
			
			// if object, set location and rotation
			if (vehicleInfo.vehicle != null) {	
				vehicleInfo.vehicle.transform.position = vehiclePos;
				vehicleInfo.vehicle.transform.rotation = Quaternion.Euler(vehicleInfo.segmentPitch, vehicleInfo.segmentHeading, 0);
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
			roadArray[0].lanesPerSide = 1;
			roadArray[0].laneSpeed1 = 40;
			roadArray[0].laneSpeed2 = 0;
			roadArray[0].laneSpeed3 = 0;
			roadArray[0].followDistance1 = 50;
			roadArray[0].followDistance2 = 0;
			roadArray[0].followDistance3 = 0;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 40;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 50;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 1;
			roadArray[2].laneSpeed1 = 40;
			roadArray[2].laneSpeed2 = 0;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 50;
			roadArray[2].followDistance2 = 0;
			roadArray[2].followDistance3 = 0;
			break;
		
		case 2:  // level 3
			roadArray[0].lanesPerSide = 1;
			roadArray[0].laneSpeed1 = 40;
			roadArray[0].laneSpeed2 = 0;
			roadArray[0].laneSpeed3 = 0;
			roadArray[0].followDistance1 = 50;
			roadArray[0].followDistance2 = 0;
			roadArray[0].followDistance3 = 0;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 40;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 50;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 1;
			roadArray[2].laneSpeed1 = 40;
			roadArray[2].laneSpeed2 = 0;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 50;
			roadArray[2].followDistance2 = 0;
			roadArray[2].followDistance3 = 0;
			break;
		
		case 3:  // level 4
			roadArray[0].lanesPerSide = 1;
			roadArray[0].laneSpeed1 = 40;
			roadArray[0].laneSpeed2 = 0;
			roadArray[0].laneSpeed3 = 0;
			roadArray[0].followDistance1 = 50;
			roadArray[0].followDistance2 = 0;
			roadArray[0].followDistance3 = 0;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 40;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 50;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 1;
			roadArray[2].laneSpeed1 = 40;
			roadArray[2].laneSpeed2 = 0;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 50;
			roadArray[2].followDistance2 = 0;
			roadArray[2].followDistance3 = 0;
			break;
		
		case 4:  // level 5
			roadArray[0].lanesPerSide = 1;
			roadArray[0].laneSpeed1 = 40;
			roadArray[0].laneSpeed2 = 0;
			roadArray[0].laneSpeed3 = 0;
			roadArray[0].followDistance1 = 50;
			roadArray[0].followDistance2 = 0;
			roadArray[0].followDistance3 = 0;
			////////////
			roadArray[1].lanesPerSide = 1;
			roadArray[1].laneSpeed1 = 40;
			roadArray[1].laneSpeed2 = 0;
			roadArray[1].laneSpeed3 = 0;
			roadArray[1].followDistance1 = 50;
			roadArray[1].followDistance2 = 0;
			roadArray[1].followDistance3 = 0;
			////////////
			roadArray[2].lanesPerSide = 1;
			roadArray[2].laneSpeed1 = 40;
			roadArray[2].laneSpeed2 = 0;
			roadArray[2].laneSpeed3 = 0;
			roadArray[2].followDistance1 = 50;
			roadArray[2].followDistance2 = 0;
			roadArray[2].followDistance3 = 0;
			break;
		}
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
