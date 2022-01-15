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

    public Track()
    {

    }
}
