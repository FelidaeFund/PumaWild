using UnityEngine;
using System.Collections;

public class CarInfo : MonoBehaviour {

	// Segment motion
	public GameObject virtualStartNode;
	public GameObject virtualTargetNode;
	private float journeyLength;
	private float distanceCovered;
    private float percentTravelled;
    private float startTime;

    // Whole path (from laneX-node0 to laneX-nodeN)
    public int startNode;
    public int endNode;
    public int currentNode;
    public Transform firstNode;
    public Transform secondNode;

	private int currentLane;
	private float virtualLaneAdjustment = -5;

	public void Start()
	{
		virtualStartNode = new GameObject();
		virtualTargetNode = new GameObject();

		virtualStartNode.transform.position = new Vector3(firstNode.position.x+virtualLaneAdjustment, firstNode.position.y, firstNode.position.z);
		virtualTargetNode.transform.position = new Vector3(secondNode.position.x+virtualLaneAdjustment, secondNode.position.y, secondNode.position.z);

		transform.position = new Vector3(virtualStartNode.transform.position.x , virtualStartNode.transform.position.y, virtualStartNode.transform.position.y);

		//virtualStartNode.transform.position = new Vector3(firstNode.position.x+5, firstNode.position.y, firstNode.position.z+5);
		//virtualTargetNode.transform.position = new Vector3(secondNode.position.x+5, secondNode.position.y, secondNode.position.z+5);

		journeyLength = Vector3.Distance(virtualStartNode.transform.position, virtualTargetNode.transform.position);
		setStartTime(Time.time);
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
	public void initializeCarPosition(Vector3 startPosition)
	{

	}

	//computes the percent travelled
	public void computePercentTravelled(float speed, float currentTime)
	{
		distanceCovered = (currentTime - startTime) * speed;
		percentTravelled = distanceCovered / journeyLength;
	}

	public void computeLaneAdjustment(float roadWidth, int numLanes)
	{

	}

	public void updatePath(Vector3 from, Vector3 to)
	{
		//Updates virtualTargetNode based on lane number and road width
		virtualStartNode.transform.position = new Vector3(from.x, from.y, from.z);
		virtualTargetNode.transform.position = new Vector3(to.x+virtualLaneAdjustment, to.y, to.z);
		//
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


}
