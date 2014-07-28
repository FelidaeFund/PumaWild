using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadInfo : MonoBehaviour {

	public bool enabled;
	public string displacementAlongAxis;

	public List<GameObject> carList;
	
	public float roadWidth;
	public int numLanes;
	public float[] laneSpeed;
	public int numNodes;
	public Transform[] nodes;
	public float averageCarsPerMinute;
	public float nextCarCreationTime;
	public GameObject partnerRoad;

}
