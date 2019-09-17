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
    public GameObject centerOfMass;
    public BillboardSprite indicator;

    private PlayerControl player;


    private void Start()
    {
        isCarActive = false;
        activePlayer = FindObjectOfType<SimpleCarController>();
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.gameObject.transform.localPosition;

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
                PlayerExit();
                // var player = FindObjectOfType<ThirdPersonCharacterControl>();
                // player.transform.position = transform.position;
                //activePlayer.transform.position = new Vector3(transform.position.x, 0, transform.position.y + 10);
                
            }
        }
    }

    public void PlayerEnter(PlayerControl player)
    {
        this.player = player;
    }

    void PlayerExit()
    {
        player.ExitCar();
        this.player = null;
    }

    public void CarActivityCheck()
    {
        /*
        if (!isCarActive)
        {
            carCamera.SetActive(false);
        }
        else
        {
            carCamera.SetActive(true);
        }
        */
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //EnterCar(carToEnter);
            Debug.Log("Player Within Collider");
            indicator.SetColor(Color.white);
            other.GetComponent<PlayerControl>().PlayerByCar(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Exited Collider");
            indicator.SetColor(Color.black);
            other.GetComponent<PlayerControl>().LeaveCar(this);
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
