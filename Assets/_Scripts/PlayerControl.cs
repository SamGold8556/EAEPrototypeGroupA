using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;

    public GameObject playerCamera;
    public CharacterController charControl;
    public SimpleCarController nearestCar
    {
        get
        {
            return _nearestCar;
        }
        set
        {
            if (_nearestCar != null)
            {
                _nearestCar.OnNotNearest();
            }
            _nearestCar = value;
            if (_nearestCar != null)
            {
                _nearestCar.OnNearest();
            }
        }
    }

    private SimpleCarController _nearestCar;
    private SimpleCarController activeCar;

    private void Update()
    {
        if (nearestCar != null && Input.GetKeyDown(KeyCode.E) && !nearestCar.isCarActive)
        {
            Debug.Log("Enter Car");
            EnterCar(nearestCar);
            //gameObject.SetActive(false);
            //EnterCar(other.gameObject.GetComponent<SimpleCarController>());
        }
    }

    void FixedUpdate ()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        Vector3 moveDir = moveSpeed * Time.fixedDeltaTime * (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"));
        moveDir.y = charControl.velocity.y + Physics.gravity.y;
        charControl.Move(moveDir);
    }

    public void EnterCar(SimpleCarController activeCar)
    {
        Debug.Log("Enter Car Triggered");
        activeCar.isCarActive = true;
        this.activeCar = activeCar;
        gameObject.SetActive(false);
        transform.parent = activeCar.centerOfMass.transform; //Connect to car, allows exit at proper location.
        activeCar.PlayerEnter(this);
        Camera.main.GetComponent<SmoothCameraMove>().SwitchTarget(activeCar.carCamera.transform);
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Car Indicators"));
    }

    public void ExitCar()
    {
        Debug.Log("CarExit Triggered");
        activeCar.isCarActive = false;
        transform.parent = null; //Disconnect from car to allow free movement.
        gameObject.SetActive(true);
        Camera.main.GetComponent<SmoothCameraMove>().SwitchTarget(playerCamera.transform);
        activeCar = null;
        nearestCar = null;
        Camera.main.cullingMask |= (1 << LayerMask.NameToLayer("Car Indicators"));
    }

    public void OnTriggerStay(Collider other)
    {
        SimpleCarController otherCar;
        if (other.GetComponentInChildren<SimpleCarController>() != null)
        {
            otherCar = other.GetComponentInChildren<SimpleCarController>();
            PlayerByCar(otherCar);
        }
    }

    public void PlayerByCar(SimpleCarController otherCar)
    {
        if (nearestCar == null ||
                Vector3.Distance(otherCar.transform.position, transform.position) < Vector3.Distance(nearestCar.transform.position, transform.position))
        {
            nearestCar = otherCar;
            Debug.Log("Near Car");
            //EnterCar(carToEnter);
        }
    }

    public void LeaveCar(SimpleCarController otherCar)
    {
        if (nearestCar == otherCar)
        {
            nearestCar = null;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<SimpleCarController>() != null)
        {
            SimpleCarController otherCar = other.GetComponentInChildren<SimpleCarController>();
            LeaveCar(otherCar);
        }
    }
}