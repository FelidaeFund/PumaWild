using UnityEngine;
using System.Collections;

public class CarInfo : MonoBehaviour {

	// Segment motion
	public GameObject virtualStartNode;
	public GameObject virtualTargetNode;
	public float journeyLength;
	public float distanceCovered;
    public float percentTravelled;
    public float startTime;

    // Whole path (from laneX-node0 to laneX-nodeN)
    public int startNode;
    public int endNode;
    public int currentNode;
    public Transform firstNode;
    public Transform secondNode;

	public int currentLane;

	public void Start()
	{
		virtualStartNode = new GameObject();
		virtualTargetNode = new GameObject();

		transform.position = new Vector3(virtualStartNode.transform.position.x+5, virtualStartNode.transform.position.y, virtualStartNode.transform.position.z+5);

		virtualStartNode.transform.position = new Vector3(firstNode.position.x+5, firstNode.position.y, firstNode.position.z+5);
		virtualTargetNode.transform.position = new Vector3(secondNode.position.x+5, secondNode.position.y, secondNode.position.z+5);

		journeyLength = Vector3.Distance(virtualStartNode.transform.position, virtualTargetNode.transform.position);
		setStartTime();
	}

	public void setStartTime()
	{
		startTime = Time.time;
	}

	public void resetPercentTravelled()
	{
		percentTravelled = 0.0f;
	}

	public void computePercentTravelled(float speed)
	{
		distanceCovered = (Time.time - startTime) * speed;
		percentTravelled = distanceCovered / journeyLength;
	}

	public float getPercentTravelled()
	{
		return percentTravelled;
	}

	public Vector3 getVirtualStartNode()
	{
		return virtualStartNode.transform.position;
	}

	public Vector3 getVirtualTargetNode()
	{
		return virtualTargetNode.transform.position;
	}

	public void updatePath(Vector3 from, Vector3 to)
	{
		//Updates virtualTargetNode based on lane number and road width
		virtualStartNode.transform.position = new Vector3(from.x, from.y, from.z);
		virtualTargetNode.transform.position = new Vector3(to.x+5, to.y, to.z);
		//
		currentNode = getNextNode();
		distanceCovered = 0.0f;
		percentTravelled = 0.0f;
		journeyLength = Vector3.Distance(virtualStartNode.transform.position, virtualTargetNode.transform.position);
		setStartTime();


	}

	public int getNextNode()
	{
		if(endNode>startNode) return currentNode+1;
		return currentNode-1;
	}


}
