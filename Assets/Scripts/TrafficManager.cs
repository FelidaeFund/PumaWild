using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour {

	public List<GameObject> roadsList;

	private Road roadInfo;
	private int amountOfCars;
	private GameObject lisfOfCars;
	private GameObject car;
	private Car carInfo;

	// Use this for initialization
	void Start () {
		roadsList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		//iterates through all the roads we have(12).
		for(int i=0; i<roadsList.Count; i++)
		{	
			//Gets the "Road" component within the specific road we are looking at.
			roadInfo = roadsList[i].GetComponent<Road>();
			//Gets the number of cars running into this specific road.
			amountOfCars = roadInfo.carList.Count;
			// Iterates 
			for(int j=0; j<amountOfCars; j++)
			{
				GameObject car = roadsList[i].GetComponent<Road>().carList[j];
				Car carInfo = car.GetComponent<Car>();
			}
		}
	}
}
