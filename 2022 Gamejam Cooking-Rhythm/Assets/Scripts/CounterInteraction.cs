using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterInteraction : MonoBehaviour
{
    public GameEvent approachStation;
    public GameEvent leaveStation;

    public bool playerAtStation = false;

    private void OnTriggerEnter(Collider other)
    {
        //Bring up counter UI 
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            playerAtStation = true;
            approachStation.Raise();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Close counter UI
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            playerAtStation = false;

            leaveStation.Raise();
        }
    }

    public void ApproachStation()
    {
        //blank for now
       
    }

    public void LeaveStation()
    {
        //blank for now

    }
}
