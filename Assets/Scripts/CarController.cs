using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    private float carX;
    private float carY;
    private float carZ;

    public Transform node;

	public Transform target;
    public GameObject virtualTarget;

    private Vector3 velocity;
    public float smoothTime = 0.1F;

    void Start() {
        virtualTarget = new GameObject();

        transform.position = new Vector3(transform.position.x+5, transform.position.y, transform.position.z);
        virtualTarget.transform.position = new Vector3(target.position.x+5, target.position.y, target.position.z);
    }

    void Update() {
        if(Mathf.Abs(transform.position.x- virtualTarget.transform.position.x)> 0.1 && Mathf.Abs(transform.position.z- virtualTarget.transform.position.z)> 0.1)
        {
            transform.position = Vector3.SmoothDamp( transform.position, virtualTarget.transform.position, ref velocity , smoothTime, 25.0f );
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
        }
    }
}
