using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public class Track
{
    public Note note;
    public List<float> beats = new List<float>();
    public List<float> durations = new List<float>();
    [Range(0f, 2f)]
    public float pitch = 1f;
    [InfoBox("Check if you want this to run always and not be a player objective")]
    public bool backgroundTrack;

    public Track()
    {

    }

    public static List<TimedNote> TracksToTimedNotes(Track[] tracks, bool isPlayer = false)
    {
        List<TimedNote> notes = new List<TimedNote>();
        foreach (Track t in tracks)
            foreach (var time in t.beats)
                notes.Add(new TimedNote(t.note, t.pitch, isPlayer, t.backgroundTrack, time));
        
        notes.Sort();
        return notes;
    }
}
