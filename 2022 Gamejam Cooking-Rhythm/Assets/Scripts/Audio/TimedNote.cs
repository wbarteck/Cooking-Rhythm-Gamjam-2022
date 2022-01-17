using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedNote : IComparer, System.IComparable
{
    public Note note;
    public float pitch;
    public bool isBackground;
    public bool isPlayer;
    public float timestamp;

    public TimedNote(Note _note, float _pitch, bool _isPlayer, bool _isBackground, float _timestamp)
    {
        note = _note;
        pitch = _pitch;
        isPlayer = _isPlayer;
        isBackground = _isBackground;
        timestamp = _timestamp;
    }

    public int Compare(object x, object y)
    {
        if (!(x is TimedNote) || !(y is TimedNote)) return 0;
        TimedNote a = (TimedNote)x;
        TimedNote b = (TimedNote)y;
        return a.timestamp.CompareTo(b.timestamp);
    }

    int System.IComparable.CompareTo(object obj)
    {
        if (!(obj is TimedNote)) return 0;
        TimedNote other = (TimedNote)obj;
        return this.timestamp.CompareTo(other.timestamp);
    }
}

