using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour {

	//===================
	// Module Variables
	//===================
	// Array with roads
	public GameObject[] roadsList;

	// Update is called once per frame
	void Update () {
		// Road data
		RoadInfo roadInfo;
		// Amount of active cars in the road
		int amountOfCars;
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
			
			// See if we need to add a car to this road
			if (Time.time >= roadInfo.nextCarCreationTime) {
				//TODO: cars instantiation logic
			
			}
			
			// Iterate through each car on this road		
			amountOfCars = roadInfo.carList.Count;
			for(int j=0; j<amountOfCars; j++)
			{
				// Points to the car GameObject
				car = roadInfo.carList[j];
				// Points to the carInfo (car's meta data).
				carInfo = car.GetComponent<CarInfo>();
				
				// increase percentTravelled based on speed and time passed
				float speed = roadInfo.laneSpeed[carInfo.getCurrentLane()]; //house-keeping
				carInfo.computePercentTravelled(speed, Time.time);

				float percentTravelled = carInfo.getPercentTravelled();
				
				// if percentTravelled > 100%
				if(percentTravelled > 1.0f)
				{
					//Checks whether the next node the car is heading to exists or not.
					int nextNode = carInfo.getNextNode();
					if(nextNode < roadInfo.numNodes && nextNode != -1)
					{
						// From current position to next node position
						carInfo.updatePath(car.transform.position, roadInfo.nodes[nextNode].position); //update segment's start and end
						car.transform.LookAt(new Vector3(carInfo.getVirtualTargetNode().x, car.transform.position.y, carInfo.getVirtualTargetNode().z));
					}

					else
					{
						car.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
					}
					
					//stops car, for now
					//car.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
					//TODO:
					// beyond end of last segment of first road
						// move car to second road
						// update startNode and endNode
						// percentTravelled -= 100%
				
					// beyond end of last segment of second road
						// destroy car
						// break;

				}
				
													
				// set position based on startNode, endNode and percentTravelled
				car.transform.position = Vector3.Lerp(carInfo.getVirtualStartNode(), carInfo.getVirtualTargetNode(), carInfo.getPercentTravelled() );
			
				// set rotation based on different cases
				
					// first half of first segment of first road

					
					// last half of last segment of first road
					
					
					// first half of first segment of second road
					
					
					// last half of last segment of second road				

					
					// normal case (none of the above)
					
			}
		}
	}
}
