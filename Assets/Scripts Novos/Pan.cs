using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {

    public bool Blocked = false;
    public float Speed = 0.05f;
    public Transform cam;

    // Use this for initialization
    void Start()
    {

    }
        // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            if (!Blocked)
            {
                transform.rotation = cam.rotation;
                transform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * Speed);
                transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * Speed, Space.World);
            }
        }
    }
}
