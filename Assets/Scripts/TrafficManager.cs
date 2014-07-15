using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour {

	public List<GameObject> roadsList;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Road roadInfo;
		int amountOfCars;
		GameObject car;
		Car carInfo;
		
		//iterates through all the roads we have(12).
		for(int i=0; i<roadsList.Count; i++)
		{	
			//Gets the "Road" component within the specific road we are looking at
			roadInfo = roadsList[i].GetComponent<Road>();
			
			// See if we need to add a car to this road
			if (Time.time() >= roadInfo.nextCarCreationTime) {

			
			}
			
			// Iterate through each car on this road			
			amountOfCars = roadInfo.carList.Count;
			for(int j=0; j<amountOfCars; j++)
			{
				GameObject car = roadsList[i].GetComponent<Road>().carList[j];
				Car carInfo = car.GetComponent<Car>();
				
				// increase percentTravelled based on time passed and speed
			
				
				// if percentTravelled > 100%
				
					// beyond end of non-last segment
						// update startNode and endNode
						// percentTravelled -= 100%
				
					// beyond end of last segment of first road
						// move car to second road
						// update startNode and endNode
						// percentTravelled -= 100%
				
					// beyond end of last segment of second road
						// destroy car
						// break;
													
				// set position based on startNode, endNode and percentTravelled
			
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
