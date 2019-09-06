using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{  

public List<AxleInfo> axleInfos; // the information about each individual axle
public float maxMotorTorque; // maximum torque the motor can apply to wheel
public float maxSteeringAngle; // maximum steer angle the wheel can have
public GameObject carCamera; // controls the camera attached to the car
public bool isCarActive; // determines whether or not player is inside car




    private void Start()
    {
        isCarActive = false;    
    }

    public void FixedUpdate()
{
        if (isCarActive)
        {
            float motor = maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
            }
        }
}

    public void Update()
    {
        CarActivityCheck();
    }

    public void CarActivityCheck()
    {
        if (!isCarActive)
        {
            carCamera.SetActive(false);
        } else
        {
            carCamera.SetActive(true);
        }
    }

}
    
[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
