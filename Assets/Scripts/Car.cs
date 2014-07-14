using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	public Transform target;
    public GameObject virtualTarget;

    public int startNode;
    public int endNode;
    public int currentNode;

    public Vector3 velocity;
    public float smoothTime = 0.1f;

	public int currentLane;

	// Use this for initialization
	void Start () {
	
	}

}
