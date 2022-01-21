using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringEvents : MonoBehaviour
{
    public GameEvent goodScore;
    public GameEvent medScore;
    public GameEvent badScore;

    public float badScoreMin = 10.1f;
    public float badScoreMax = 20.0f;
    public float medScoreMin = 10.1f;
    public float medScoreMax = 4.9f;
    public float goodScoreMin = 0.1f;
    public float goodScoreMax = 5.0f;

    TrackComparison trackComparison;

    private void Awake()
    {
        GetComponent<TrackComparison>();
    }

    public void CompareScore(float score)
    {      

        if(score >= badScoreMin)
        {
            badScore.Raise();
        }

        if(score >= medScoreMax && score <= medScoreMin)
        {
            medScore.Raise();
        }

        if(score >= goodScoreMin && score <= goodScoreMax)
        {
            goodScore.Raise();
        }
    }

}
