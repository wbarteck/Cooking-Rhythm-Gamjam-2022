using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

public class LoopingPlayer : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    //[SerializeField] float playerVolume = 0f;
    [SerializeField] float orderVolume = 0f;
    [SerializeField] float currentTime;

    TimedNote nextNote;
    Queue<TimedNote> noteQueue;

    [Button] public void StartPlayhead(Melody order, float startTime = 0f)
    {
        StopAllCoroutines();
        StartCoroutine(Playhead(order, startTime));
    }

    IEnumerator Playhead(Melody order, float startTime = 0f)
    {
        List<TimedNote> notes = Track.TracksToTimedNotes(order.tracks);
        noteQueue = new Queue<TimedNote>(notes);
        
        nextNote = noteQueue.Dequeue();
        currentTime = startTime;
        while (currentTime > nextNote.timestamp) nextNote = noteQueue.Count > 0 ? noteQueue.Dequeue() : null;

        while (true)
        {
            // if notes are finished and time should loop, reset
            while (nextNote == null && currentTime >= order.TotalSeconds)
            {
                currentTime %= order.TotalSeconds;
                noteQueue = new Queue<TimedNote>(notes);
                nextNote = noteQueue.Dequeue();
            }
            // trigger all applicable notes if the playhead is at the notes' timestamp
            while (nextNote != null && currentTime >= nextNote.timestamp)
            {
                // play note
                AudioSource source = AudioSourcePool.instance.GetAudioSource();
                UseAudioSource(nextNote, source);
                nextNote = noteQueue.Count > 0 ? noteQueue.Dequeue() : null;
            }
            // wait for next frame, increment playhead
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    async void UseAudioSource(TimedNote tn, AudioSource source)
    {
        source.clip = tn.note.cookingNote;
        source.pitch = tn.pitch;
        source.volume = tn.note.volume * orderVolume; // FIXME
        source.Play();
        // release audio source to pool ocne the sound is finished playing
        await Task.Delay((int)(tn.note.cookingNote.length * 1000));
        if (source != null) AudioSourcePool.instance.ReleaseAudioSource(source);
    }
}

