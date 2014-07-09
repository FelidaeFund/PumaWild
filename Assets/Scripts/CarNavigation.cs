using UnityEngine;
using System.Collections;

public class CarNavigation : MonoBehaviour {

	public Transform target;
    public float smoothTime = 1000000.0F;
    private float xVelocity = 0.01F;
    private float yVelocity = 0.01F;
    private float zVelocity = 0.01F;

    void Update() {
        float newPositionX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref xVelocity, smoothTime);
        float newPositionY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
        float newPositionZ = Mathf.SmoothDamp(transform.position.z, target.position.z, ref zVelocity, smoothTime);
        transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);
    }
}
