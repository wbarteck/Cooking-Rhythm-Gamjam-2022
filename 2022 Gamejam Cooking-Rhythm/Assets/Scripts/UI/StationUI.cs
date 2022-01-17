using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationUI : MonoBehaviour
{
    public CounterInteraction station;
    public GameObject[] Objects;

    private void Awake()
    {
        //TODO automatically populate Objects array.
    }


    public void TurnOffObjects()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(false);
        }
    }

    public void TurnOnObjects()
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].SetActive(true);
        }
    }
}
