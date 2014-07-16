using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour {

	// Module variables
	public GameObject[] roadsList;

	// Update is called once per frame
	void Update () {
		RoadInfo roadInfo;
		int amountOfCars;
		GameObject car;
		CarInfo carInfo;
		
		//iterates through all the roads we have(12).
		for(int i=0; i<roadsList.Length; i++)
		{	
			//Gets the "Road" component within the specific road we are looking at
			roadInfo = roadsList[i].GetComponent<RoadInfo>();
			
			// See if we need to add a car to this road
			if (Time.time >= roadInfo.nextCarCreationTime) {
				//TODO: cars instantiation logic
			
			}
			
			//Debug.Log(roadInfo.carList.Count);
			// Iterate through each car on this road		
			amountOfCars = roadInfo.carList.Count;
			for(int j=0; j<amountOfCars; j++)
			{
				// Points to the car GameObject
				//Debug.Log(roadInfo.carList[j]);
				car = roadInfo.carList[j];
				// Points to the carInfo (car's meta data).
				carInfo = car.GetComponent<CarInfo>();
				
				// increase percentTravelled based on time passed and speed
				float speed = roadInfo.laneSpeed[carInfo.currentLane]; //house-keeping
				carInfo.computePercentTravelled(speed);
				float percentTravelled = carInfo.getPercentTravelled();
				
				// if percentTravelled > 100%
				if(percentTravelled > 1.0f)
				{
					//Checks whether the next node the car is heading to exists or not.
					int nextNode = carInfo.getNextNode();
					if(nextNode <= roadInfo.numNodes && nextNode != -1)
					{
						carInfo.updatePath(car.transform.position, roadInfo.nodes[nextNode].position); //update segment's start and end
					}
					
					//stops car, for now
					car.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
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
