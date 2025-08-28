using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbit : MonoBehaviour {

    public bool Blocked = false;
    public Transform target;
    public float distance = 4.0f;
    [HideInInspector]
    public Vector3 DistanceVector;
    public RaycastHit hit;
    public bool UseRaycast = false;

    public float xSpeed = 30.0f;
    public float ySpeed = 60.0f;

    public float yMinLimit = -80f;
    public float yMaxLimit = 80f;

    public float distanceMin = 0.5f;
    public float distanceMax = 10.0f;

    public float PanSpeed = 1.0f;

    [HideInInspector]
    public float x = 0.0f;
    [HideInInspector]
    public float y = 0.0f;
    [HideInInspector]
    public float xpan = 0.0f;
    [HideInInspector]
    public float ypan = 0.0f;

    // Use this for initialization
    void Start () {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (gameObject.GetComponent<Rigidbody>())
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (!Blocked)
        {
            if (target && Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
            if (target)
            {
                y = ClampAngle(y, yMinLimit, yMaxLimit);

                var rotation = Quaternion.Euler(y, x, 0);

                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distanceMin, distanceMax);

                if (UseRaycast)
                {
                    if (Physics.Raycast(new Ray(target.position, transform.position), out hit))
                    {
                        distance -= hit.distance;
                    }
                }
                DistanceVector = new Vector3(0.0f, 0.0f, -distance);
                var position = rotation * DistanceVector + target.position;

                transform.rotation = rotation;
                transform.position = position;

            }
        }
    }

    public float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
