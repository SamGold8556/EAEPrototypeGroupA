using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerGenerator : MonoBehaviour
{
    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider> { }
    public TriggerEvent TriggerEnter;
    public TriggerEvent TriggerStay;
    public TriggerEvent TriggerExit;


    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerStay.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit.Invoke(other);
    }
}
