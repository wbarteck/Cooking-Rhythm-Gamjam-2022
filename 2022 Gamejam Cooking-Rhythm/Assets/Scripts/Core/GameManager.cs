using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        Idle,
        Composition,
        Judging,
        End
    }
    public GameState gameState;

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

[System.Serializable]
public class Course
{
    public Melody order;
    public float timeLimit;
}