using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    public GameObject playerCamera;
    public float Speed;
    public bool inCar;
    public bool nearCar;
    public GameObject carToEnter;

    private void Start()
    {
    }

    void Update ()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (!inCar)
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
            if (nearCar && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Enter Car");
                GameManager.instance.EnterCar();
                //gameObject.SetActive(false);
                //EnterCar(other.gameObject.GetComponent<SimpleCarController>());
            }
            nearCar = false;
        }      
    }

    public void EnterCar(SimpleCarController activeCar)
    {
        activeCar.isCarActive = true;
        playerCamera.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Vehicle")
        {
            nearCar = true;
            Debug.Log("Near Car");
            //EnterCar(carToEnter);
        }
    }
}