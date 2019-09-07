using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Vehicle;
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void EnterCar()
    {
        Debug.Log("Enter Car Triggered");
        Vehicle.gameObject.GetComponent<SimpleCarController>().isCarActive = true;
        Player.gameObject.GetComponent<ThirdPersonCharacterControl>().inCar = true;
        Player.SetActive(false);
        Vehicle.GetComponent<SimpleCarController>().carCamera.SetActive(true);
        Vehicle.GetComponent<SimpleCarController>().isCarActive = true;
    }

    public void ExitCar()
    {
        Debug.Log("CarExit Triggered");
        Vehicle.gameObject.GetComponent<SimpleCarController>().isCarActive = false;
        Player.gameObject.GetComponent<ThirdPersonCharacterControl>().inCar = false;
        Player.SetActive(true);
        Vehicle.GetComponent<SimpleCarController>().carCamera.SetActive(true);
        Vehicle.GetComponent<SimpleCarController>().isCarActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}