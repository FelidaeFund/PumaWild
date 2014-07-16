using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadInfo : MonoBehaviour {

	public List<GameObject> carList;
	
	public int width;
	public int numLanes;
	public float[] laneSpeed;
	public int numNodes;
	public Transform[] nodes;
	public float averageCarsPerMinute;
	public float nextCarCreationTime;
	public GameObject partnerRoad;

	// Use this for initialization
	void Start () {
	}
	

}
