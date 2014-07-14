using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Road : MonoBehaviour {

	public int numLanes;
	public int width;

	public int numNodes;
	public Transform[] nodes;

	public List<GameObject> carList;

	// Use this for initialization
	void Start () {
		nodes = new Transform[5];
		numNodes = nodes.Length;
		carList = new List<GameObject>();
	}

}
