using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour
{
    CounterInteraction[] stations;
    public StationUI[] stationUIs;

    CounterInteraction currentStation;

    private void Start()
    {
        stations = FindObjectsOfType<CounterInteraction>();
        for (int i = 0; i < stationUIs.Length; i++)
        {
            stationUIs[i].TurnOffObjects();
        }
    }

    public void WhatStationAmIAt()
    {
        for (int i = 0; i < stations.Length; i++)
        {
            if(stations[i].playerAtStation == true)
            {
                currentStation = stations[i];
                for (int j = 0; j < stationUIs.Length; j++)
                {
                    Debug.Log("stationUIs looping");
                    //Wire stations to correct UI in Editor Inspector
                    if(stationUIs[j].station.Equals(currentStation))
                    {
                        Debug.Log("turn on objects called");
                        stationUIs[j].TurnOnObjects();
                    }
                }
            }
        }
    }

    public void LeftStation()
    {
        for (int i = 0; i < stationUIs.Length; i++)
        {
            stationUIs[i].TurnOffObjects();
        }
    }

}
