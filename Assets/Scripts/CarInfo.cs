﻿using UnityEngine;
using System.Collections;

public class CarInfo : MonoBehaviour {

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


	//=======================
	// Methods
	//=======================
	//THIS SHOULD REMOVE THE Start() function
	public void setJourney(Transform newCarFirstNode, Transform newCarSecondNode, int newCarStartNode, int newCarEndNode, float roadWidth, int roadLanes, int newCarLane, float carMaxSpeed)
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
	    virtualStartNode.transform.position = new Vector3(newCarFirstNode.position.x+virtualLaneAdjustment, newCarFirstNode.position.y, newCarFirstNode.position.z);
		virtualTargetNode.transform.position = new Vector3(newCarSecondNode.position.x+virtualLaneAdjustment, newCarSecondNode.position.y, newCarSecondNode.position.z);
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
	public void computeLaneAdjustment(float roadWidth, int numLanes)
	{
		float laneWidth = roadWidth/numLanes;

		if(currentLane <= numLanes/2)
		{
			virtualLaneAdjustment = -(((numLanes/2 - currentLane)*laneWidth) + laneWidth/2);
		}
		else
		{
			virtualLaneAdjustment = +(((numLanes/2 - currentLane)*laneWidth) + laneWidth/2);
		}
	}

	public void updatePath(Vector3 from, Vector3 to)
	{
		//Updates virtualTargetNode based on lane number and road width
		virtualStartNode.transform.position = new Vector3(from.x, from.y, from.z);
		virtualTargetNode.transform.position = new Vector3(to.x+virtualLaneAdjustment, to.y, to.z);
		// Set the car in the next node (begining of the segment)
		currentNode = getNextNode();
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
