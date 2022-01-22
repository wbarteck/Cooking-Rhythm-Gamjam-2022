using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

public class LoopingPlayer : MonoBehaviour
{
    [SerializeField] bool isPlaying;
    //[SerializeField] float playerVolume = 0f;
    [SerializeField] float orderVolume = 1f;
    [SerializeField] float playerVolume = 1f;
    [SerializeField] float currentTime;
    public float GetCurrentTime { get { return currentTime; } }


    Melody currentMelody;
    TimedNote nextNote;
    Queue<TimedNote> noteQueue;

    public float Progress { get { return(currentMelody != null) ? Mathf.Clamp01(currentTime / currentMelody.TotalSeconds) : 0f; } }

    [Button]
    public void StopPlayhead()
    {
        currentTime = 0f;
        StopAllCoroutines();
    }
    [Button] public void StartPlayhead(Melody order, float startTime = 0f)
    {
        currentMelody = order;
        StopAllCoroutines();
        StartCoroutine(Playhead(order, startTime));
    }
    public void UpdateBeat()
    {
        if (currentMelody == null) return;
        // re-trigger the playhead coroutine
        // this forces a refresh on the notes queue to play
        StopAllCoroutines();
        StartCoroutine(Playhead(currentMelody, currentTime));
    }


    IEnumerator Playhead(Melody order, float startTime = 0f)
    {
        List<TimedNote> notes = Track.TracksToTimedNotes(order.tracks);
        // add player notes
        notes.AddRange(Station.PlayerNotes());
        notes.Sort();
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
        if (tn.isBackground) // always play
            source.volume = tn.note.volume;
        else
            source.volume = tn.note.volume * ((tn.isPlayer) ? playerVolume : orderVolume);
        source.Play();
        // release audio source to pool ocne the sound is finished playing
        await UniTask.Delay((int)(tn.note.cookingNote.length * 1000));
        if (source != null) AudioSourcePool.instance.ReleaseAudioSource(source);
    }

    public void SoloOrderAudio()
    {
        playerVolume = 0f;
        orderVolume = 1f;
    }
    public void SoloPlayerAudio()
    {
        playerVolume = 1f;
        orderVolume = 0f;
    }

    public void PlayBoth()
    {
        playerVolume = 1f;
        orderVolume = 1f;
    }
}

