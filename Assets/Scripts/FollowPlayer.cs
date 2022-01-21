using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float smoothness;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    GameObject CameraPositionFirstPersonn, CameraPositionThirdPersonn;

    private Vector3 velocity = Vector3.zero;
    private bool thirdPersonn = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            changeCamera();
        }
    }

    void FixedUpdate()
    {
        if (thirdPersonn)
        {
            offset = new Vector3(0, 5, -10);
            smoothness = 0.1f;
        }
        else
        {
            offset = new Vector3(0, 2, 3);
            smoothness = 0;
        }

        // Target position
        Vector3 positionToGo = target.position + offset;
        // Set the position to the target with a SmoothDamp to make a smooth movement
        transform.position = Vector3.SmoothDamp(transform.position, positionToGo, ref velocity, smoothness);
    }


    void changeCamera()
    {
        if (thirdPersonn)
        {
            thirdPersonn = false;
            transform.position = CameraPositionFirstPersonn.transform.position;
        }
        else
        {
            thirdPersonn = true;
            transform.position = CameraPositionThirdPersonn.transform.position;
        }
            
    }
}
