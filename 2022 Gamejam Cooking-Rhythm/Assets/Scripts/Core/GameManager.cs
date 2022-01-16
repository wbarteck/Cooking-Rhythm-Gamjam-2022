using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Melody currentOrder;


    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }
}
