using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour {

	//===================
	// Module Variables
	//===================
	// Array with roads
	public GameObject[] roadsList;

	public GameObject suvModel;


	private Transform newCarFirstNode;
	private Transform newCarSecondNode;
	private int newCarStartNode;
	private int newCarEndNode;



	// Update is called once per frame
	void Update () {
		// Road data
		RoadInfo roadInfo;
		// Amount of active cars in the road
		int amountOfCars;
		int numberOfNodes;
		// Points to the game object
		GameObject car;
		// Points to the gamObject's component "CarInfo", which stores data about the car.
		CarInfo carInfo;
		
		//iterates through all the roads we have(12).
		for(int i=0; i<roadsList.Length; i++)
		{	
			//Gets the "RoadInfo" component within the specific road we are looking at
			roadInfo = roadsList[i].GetComponent<RoadInfo>();
			// Checks whether this road is enabled or not
			if (roadInfo.enabled == false) break;

			numberOfNodes = roadInfo.nodes.Length;
			
			// See if we need to add a car to this road
			if (Time.time >= roadInfo.nextCarCreationTime) {
				GameObject newCar = createNewCar(roadInfo, numberOfNodes);
				roadInfo.carList.Add(newCar);
				//Debug.Log(roadInfo.carList);
				roadInfo.nextCarCreationTime = Time.time + 60/roadInfo.averageCarsPerMinute;
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
					//Checks whether the next node the car is heading to exists or not.
					int nextNode = carInfo.getNextNode();
					if(nextNode < numberOfNodes && nextNode != -1)
					{
						// Updates car path: sets segment from current position to next node's position
						carInfo.updatePath(car.transform.position, roadInfo.nodes[nextNode].position); //update segment's start and end
						// Rotates Y-axis to point the car towards its target
						// place-holder for rotation algorithm, that will be handled outside of this if
						carInfo.lookAtNextNode();
					}

					else
					{
						// Gets the data about the partner road
						RoadInfo partnerInfo = roadInfo.partnerRoad.GetComponent<RoadInfo>();
						int partnerLength = partnerInfo.nodes.Length;

						// We are going down (N to 0)
						// And the car is at the border 0, now.
						if(nextNode == -1)
						{
							// Computes distance between this border and the begining of next road
							float nodesDistance = Vector3.Distance(car.transform.position, partnerInfo.nodes[partnerLength-1].position);
							if(nodesDistance<50f)
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
		}
	}

	private GameObject createNewCar(RoadInfo roadInfo, int numberOfNodes)
	{
		GameObject newCar = Instantiate(suvModel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		CarInfo newCarInfo = newCar.GetComponent<CarInfo>();

		// Getting information necessary to set car journey
		float roadWidth = roadInfo.roadWidth;
		int roadLanes = roadInfo.numLanes;
		int newCarLane = Random.Range(1, roadLanes+1);

		// Gets max speed alowed by the lane this car will move at
		float carMaxSpeed = roadInfo.laneSpeed[newCarLane-1];

		// Decides whether car is going "up" or "down" the road
		// First half of the lanes go from node 0 to last node. (up)
		// Second half of the lanes go from last node to 0. (down)
		if(newCarLane <= roadLanes/2)
		{
			newCarFirstNode = roadInfo.nodes[0];
			newCarSecondNode = roadInfo.nodes[1];
			newCarStartNode = 0;
			newCarEndNode = numberOfNodes-1;
		}
		else
		{
			newCarFirstNode = roadInfo.nodes[numberOfNodes-1];
			newCarSecondNode = roadInfo.nodes[numberOfNodes-2];
			newCarStartNode = numberOfNodes-1;
			newCarEndNode = 0;
		}

		// Sets the configuration above to the new car
		newCarInfo.setJourney(newCarFirstNode, newCarSecondNode, newCarStartNode, newCarEndNode, roadWidth, roadLanes, newCarLane, carMaxSpeed);
		
		// returns a pointer to the new car GameObject
		return newCar;
	}

	private void destroyCar(CarInfo carInfo, RoadInfo roadInfo, int carId, GameObject car)
	{
		carInfo.stopCar();
		carInfo.destroyNodes();
		roadInfo.carList.RemoveAt(carId);
		Destroy(car);
	}
}
