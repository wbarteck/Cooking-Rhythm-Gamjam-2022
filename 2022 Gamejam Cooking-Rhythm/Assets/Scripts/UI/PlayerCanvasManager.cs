using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour
{
    CounterInteraction[] stations;

    CounterInteraction currentStation;

    private void Awake()
    {
        stations = FindObjectsOfType<CounterInteraction>();
    }

    public void WhatStationAmIAt()
    {
        for (int i = 0; i < stations.Length; i++)
        {
            if(stations[i].playerAtStation == true)
            {
                currentStation = stations[i];
            }
        }
    }

}
