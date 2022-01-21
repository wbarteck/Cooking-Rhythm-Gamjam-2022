using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TrackComparison : MonoBehaviour
{
    public TMP_Text scoreText;

    float tolerance = .1f; //notes within X second count as perfect timing

    public GameEvent goodScore;
    public GameEvent medScore;
    public GameEvent badScore;

    public float badScoreMin = 10.1f;
    public float badScoreMax = 20.0f;
    public float medScoreMin = 10.1f;
    public float medScoreMax = 4.9f;
    public float goodScoreMin = 0.1f;
    public float goodScoreMax = 5.0f;
    public void Score()
    {
        Melody customerOrder = GameManager.instance.currentOrder;
        Track[] playerTracks = FindObjectsOfType<Station>(true).Select(s => s.track).ToArray();

        float distance = 0f;
        foreach(Track t in customerOrder.tracks)
        {
            if (t.backgroundTrack) continue;
            // find matching player track
            var playerTrack = playerTracks.Where(track => track.note == t.note).First();
            distance += CompareTracks(t, playerTrack);
        }
        Debug.Log($"Total Distance: {distance}");
        scoreText.text = $"Higher numer = worse performance: {distance}";
        CompareScore(distance);
    }

    public float CompareTracks(Track a, Track b)
    {
        float noteDifference = Mathf.Abs(a.beats.Count - b.beats.Count);
        float distance = 0f;
        foreach (float n in b.beats)
        {
            var distanceList = a.beats.Select(x => Mathf.Abs(x - n));
            var closestNote = distanceList.Min();

            if (closestNote > tolerance) distance += (closestNote - tolerance);

        }
        return (noteDifference * 1f) + distance;
    }

    public void CompareScore(float score)
    {

        if (score >= badScoreMin)
        {
            badScore.Raise();
        }

        if (score >= medScoreMax && score <= medScoreMin)
        {
            medScore.Raise();
        }

        if (score >= goodScoreMin && score <= goodScoreMax)
        {
            goodScore.Raise();
        }
    }
}
