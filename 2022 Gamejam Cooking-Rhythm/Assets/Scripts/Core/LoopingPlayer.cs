using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

public class LoopingPlayer : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    [SerializeField] float playerVolume = 0f;
    [SerializeField] float orderVolume = 0f;
    [SerializeField] float currentTime;

    TimedNote nextNote;
    Queue<TimedNote> noteQueue;

    [Button] public void StartPlayhead(Melody order, Melody player, float startTime = 0f)
    {
        StopAllCoroutines();
        StartCoroutine(Playhead(order, player, startTime));
    }

    IEnumerator Playhead(Melody order, Melody player, float startTime = 0f)
    {
        List<TimedNote> notes = new List<TimedNote>();
        foreach(Track t in order.tracks)
            foreach(var time in t.beats)
                notes.Add(new TimedNote(t.note, t.pitch, false, false, time));
        foreach (Track t in player.tracks)
            foreach (var time in t.beats)
                notes.Add(new TimedNote(t.note, t.pitch, true, false, time));
        notes.Sort();
        noteQueue = new Queue<TimedNote>(notes);
        nextNote = noteQueue.Dequeue();
        currentTime = startTime;
        while (currentTime > nextNote.timestamp) nextNote = noteQueue.Count > 0 ? noteQueue.Dequeue() : null;

        while (true)
        {
            if (nextNote == null && currentTime >= order.TotalSeconds)
            {
                currentTime %= order.TotalSeconds;
                noteQueue = new Queue<TimedNote>(notes);
                nextNote = noteQueue.Dequeue();
            }
            if (nextNote != null && currentTime >= nextNote.timestamp)
            {
                // play note
                AudioSource source = AudioSourcePool.instance.GetAudioSource();
                UseAudioSource(nextNote, source);
                nextNote = noteQueue.Count > 0 ? noteQueue.Dequeue() : null;
            }
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    async void UseAudioSource(TimedNote tn, AudioSource source)
    {
        source.clip = tn.note.cookingNote;
        source.pitch = tn.pitch;
        source.Play();
        // check for null in case we stop or quit while playing
        await Task.Delay((int)(tn.note.cookingNote.length * 1000));
        if (source != null) AudioSourcePool.instance.ReleaseAudioSource(source);
    }
}

public class TimedNote: IComparer, System.IComparable
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
