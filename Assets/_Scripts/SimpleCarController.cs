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
    private SimpleCarController activePlayer;




    private void Start()
    {
        isCarActive = false;
        activePlayer = FindObjectOfType<SimpleCarController>();
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

        if (isCarActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // var player = FindObjectOfType<ThirdPersonCharacterControl>();
                // player.transform.position = transform.position;
                //activePlayer.transform.position = new Vector3(transform.position.x, 0, transform.position.y + 10);
                GameManager.instance.ExitCar();
            }
        }
    }

    public void CarActivityCheck()
    {
        if (!isCarActive)
        {
            carCamera.SetActive(false);
        }
        else
        {
            carCamera.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //EnterCar(carToEnter);
            Debug.Log("Player Within Collider");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Enter Car");

                if (!isCarActive)
                {
                    GameManager.instance.Vehicle = this.gameObject;
                    Debug.Log("Car is not active");
                    GameManager.instance.EnterCar();
                }
                //else
                //{
                //    GameManager.instance.ExitCar();
                //  }

                //gameObject.SetActive(false);
                //EnterCar(other.gameObject.GetComponent<SimpleCarController>());
            }
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
