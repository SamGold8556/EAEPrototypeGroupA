using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMove : MonoBehaviour
{
    public float transitionDuration;
    public Transform target;

    private Vector3 oldPosition;
    private Quaternion oldRotation;

    //Counts from transitionDuration down to 0 during a transition.
    private float transitionTimer;

    public void SwitchTarget(Transform newTarget)
    {
        oldPosition = transform.position;
        oldRotation = transform.rotation;
        target = newTarget;
        transitionTimer = transitionDuration;
    }

    private void Update()
    {
        if (transitionTimer > 0.0f)
        {
            transform.position = Vector3.Slerp(oldPosition, target.position, 1 - (transitionTimer / transitionDuration));
            transform.rotation = Quaternion.Slerp(oldRotation, target.rotation, 1 - (transitionTimer / transitionDuration));
            transitionTimer -= Time.deltaTime;
        }
        else
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
