using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour {

	//===================
	// Module Variables
	//===================
	// Array with roads



	private Transform newCarFirstNode;
	private Transform newCarSecondNode;
	private int newCarStartNode;
	private int newCarEndNode;
	

	private class RoadInfo {
		public int numLanes;
		public float roadWidth;
		public float averageSpeed;
		public float averageCarsPerMinute;
		public float nextCarCreationTime;
		public string displacementAlongAxis;
		public GameObject[] nodeList;
		public GameObject[] carList;
	}
	
	private RoadInfo[] roadArray;
	
	public GameObject[] road1Nodes;
	public GameObject[] road2Nodes;
	public GameObject[] road3Nodes;
	
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
}
