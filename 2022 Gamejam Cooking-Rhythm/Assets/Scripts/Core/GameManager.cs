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
    [SerializeField] GameEvent goToIntro;
    [SerializeField] GameEvent goToIdle;
    [SerializeField] GameEvent goToPlaceOrder;
    [SerializeField] GameEvent goToComposition;
    [SerializeField] GameEvent goToJudging;
    [SerializeField] GameEvent goToEnd;
    [Title("Game Object References")]
    [SerializeField] LoopingPlayer playhead;

    public enum GameState
    {
        Intro,
        Idle,
        PlaceOrder,
        Composition,
        Judging,
        End
    }
    public GameState gameState;

    public Melody currentOrder;
    public float introTime = 1.0f;

    private void Start()
    {
        instance = this;

        gameState = GameState.Intro;
        goToIntro.Raise();
        StartCoroutine(StartGameAfterIntro());
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void StartGamePlay()
    {
        gameState = GameState.Idle;
        goToIdle.Raise();

    }

    IEnumerator StartGameAfterIntro()
    {
        yield return new WaitForSeconds(introTime);
        StartGamePlay();
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

    public void RoundComplete()
    {
        gameState = GameState.Judging;
        goToJudging.Raise();
    }

    public void GameOver()
    {
        goToEnd.Raise();
    }
}

[System.Serializable]
public class Course
{
    public Melody order;
    public float timeLimit;
}