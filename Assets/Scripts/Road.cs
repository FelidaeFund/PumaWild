using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Road : MonoBehaviour {

	public List<GameObject> carList;
	
	public int width;
	public int numLanes;
	public int numNodes;
	public Transform[] nodes;
	public averageCarsPerMinute;
	public nextCarCreationTime;
	public Road partnerRoad;

	// Use this for initialization
	void Start () {
		nodes = new Transform[5];
		numNodes = nodes.Length;
		carList = new List<GameObject>();
		SelectNextCarCreationTime();
	}
	

}
