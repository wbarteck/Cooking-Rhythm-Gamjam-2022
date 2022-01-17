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
        approachStation.Raise();
    }

    private void OnTriggerExit(Collider other)
    {
        //Close counter UI
        leaveStation.Raise();
    }

    public void ApproachStation()
    {
        playerAtStation = true;
    }

    public void LeaveStation()
    {
        playerAtStation = false;
    }
}
