using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TrackComparison : MonoBehaviour
{
    public TMP_Text scoreText;

    float tolerance = .01f; //notes within X second count as perfect timing

    public GameEvent goodScore;
    public GameEvent medScore;
    public GameEvent badScore;

    public float badScoreStartPercent = 0.5f; // anything above 50% distance is bad
    public float medScoreStartPercent = 0.25f;// anything between 25% an 50% is medium
    public float goodScoreStartPercent = 0f;    // anything else is good

    public void Score()
    {
        Melody customerOrder = GameManager.instance.currentOrder;
        Track[] playerTracks = FindObjectsOfType<Station>(true).Select(s => s.track).ToArray();

        float distance = 0f;
        int totalNotes = 0;
        foreach(Track t in customerOrder.tracks)
        {
            if (t.backgroundTrack) continue;
            // find matching player track
            totalNotes += t.beats.Count;
            var playerTrack = playerTracks.Where(track => track.note == t.note).First();
            distance += CompareTracks(t, playerTrack);
        }
        Debug.Log($"Total Distance: {distance}");
        var scorePercent = distance / totalNotes;
        Debug.Log($"Score Percent: {scorePercent}");
        scoreText.text = $"You Got {Mathf.Round((1f - scorePercent) * 100)}% of the notes correct";

        CompareScore(scorePercent);
    }

    public float CompareTracks(Track a, Track b)
    {
        float noteDifference = Mathf.Abs(a.beats.Count - b.beats.Count);
        float distance = 0f;
        foreach (float n in b.beats)
        {
            var distanceList = a.beats.Select(x => Mathf.Abs(x - n));
            var closestNote = distanceList.Min();

            if (closestNote > tolerance) distance += Mathf.Clamp01(closestNote - tolerance);

        }
        return (noteDifference) + distance;
    }

    public void CompareScore(float scorePercent)
    {

        if (scorePercent >= badScoreStartPercent)
        {
            Debug.Log("Bad Score");
            badScore.Raise();
        }
        else if (scorePercent >= medScoreStartPercent)
        {
            Debug.Log("Medium Score");
            medScore.Raise();
        }
        else
        {
            Debug.Log("Good Score");
            goodScore.Raise();
        }
    }
}
