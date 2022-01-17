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
    [Range(-1f, 1f)]
    public float pitch = 1f;
    [InfoBox("Check if you want this to run always and not be a player objective")]
    public bool backgroundTrack;

    public Track()
    {

    }
}
