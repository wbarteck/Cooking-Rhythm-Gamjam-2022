using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverLight : MonoBehaviour
{
    Light stationLight;

    private void Start()
    {
        stationLight = GetComponentInChildren<Light>();
        stationLight.enabled = false;
    }

    private void OnMouseOver()
    {
        stationLight.enabled = true;
    }

    private void OnMouseExit()
    {
        stationLight.enabled = false;

    }
}
