using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Title("Event References")]
    [SerializeField] GameEvent goToIdle;
    [SerializeField] GameEvent goToPlaceOrder;
    [SerializeField] GameEvent goToComposition;
    [SerializeField] GameEvent goToJudging;
    [SerializeField] GameEvent goToEnd;
    [Title("Game Object References")]
    [SerializeField] LoopingPlayer playhead;

    public enum GameState
    {
        Idle,
        PlaceOrder,
        Composition,
        Judging,
        End
    }
    public GameState gameState;

    public Melody currentOrder;


    private void Awake()
    {
        instance = this;

        gameState = GameState.Idle;
        goToIdle.Raise();
    }

    private void OnDisable()
    {
        instance = null;
    }

    public async void PlaceOrder(Melody order)
    {
        gameState = GameState.PlaceOrder;
        goToPlaceOrder.Raise();
        currentOrder = order;

        // delay a X seconds
        await UniTask.Delay(Mathf.CeilToInt(1000 * 1.5f));
        playhead.StartPlayhead(order, 0f);
        await UniTask.Delay(Mathf.CeilToInt(order.TotalSeconds * 1000));

        // start composition
        gameState = GameState.Composition;
        goToComposition.Raise();
    }
}

[System.Serializable]
public class Course
{
    public Melody order;
    public float timeLimit;
}