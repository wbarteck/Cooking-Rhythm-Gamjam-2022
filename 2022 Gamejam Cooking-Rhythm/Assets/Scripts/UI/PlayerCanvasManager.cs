using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PlayerCanvasManager : MonoBehaviour
{
    public static PlayerCanvasManager instance;

    [SerializeField] Slider pitchSlider;
    [SerializeField] Button beatButton;
    [SerializeField] Button clearButton;

    [SerializeField] GameObject[] CompositionUI;

    // 'public' inspector arrays don't scale well when you have to manually fill them
    Station[] stations;
    [ShowInInspector, ReadOnly] Station currentStation;

    private void Awake()
    {
        stations = FindObjectsOfType<Station>();
    }

    private void OnEnable()
    {
        instance = this;

        foreach (var g in CompositionUI)
            g.SetActive(false);


    }
    private void OnDisable()
    {
        instance = null;
    }

    public void ActivateStation(Station station)
    {
        currentStation = station;

        foreach (var g in CompositionUI)
            g.SetActive(true);

        pitchSlider.value = station.track.pitch;

        pitchSlider.onValueChanged.RemoveAllListeners();
        beatButton.onClick.RemoveAllListeners();
        clearButton.onClick.RemoveAllListeners();

        pitchSlider.onValueChanged.AddListener(station.AdjustPitch);
        beatButton.onClick.AddListener(station.AddBeat);
        clearButton.onClick.AddListener(station.ClearNotes); 
    }

    public void LeaveStation(Station station)
    {
        if (currentStation != station) return; // cant leave a station that youre not at
       
        currentStation = null;

        // hide ui
        foreach (var g in CompositionUI)
            g.SetActive(false);
    }



    // no
    //CounterInteraction[] stations;
    //public StationUI[] stationUIs;

    //CounterInteraction currentStation;

    //private void Start()
    //{
    //    stations = FindObjectsOfType<CounterInteraction>();
    //    for (int i = 0; i < stationUIs.Length; i++)
    //    {
    //        stationUIs[i].TurnOffObjects();
    //    }
    //}

    //public void WhatStationAmIAt()
    //{
    //    for (int i = 0; i < stations.Length; i++)
    //    {
    //        if(stations[i].playerAtStation == true)
    //        {
    //            currentStation = stations[i];
    //            for (int j = 0; j < stationUIs.Length; j++)
    //            {
    //                Debug.Log("stationUIs looping");
    //                //Wire stations to correct UI in Editor Inspector
    //                if(stationUIs[j].station.Equals(currentStation))
    //                {
    //                    Debug.Log("turn on objects called");
    //                    stationUIs[j].TurnOnObjects();
    //                }
    //            }
    //        }
    //    }
    //}

    //public void LeftStation()
    //{
    //    for (int i = 0; i < stationUIs.Length; i++)
    //    {
    //        stationUIs[i].TurnOffObjects();
    //    }
    //}

}
