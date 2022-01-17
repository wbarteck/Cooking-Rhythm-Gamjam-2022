using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class MelodyPlayer : MonoBehaviour
{
    public Melody melody;

    [DisableIf("@true"), Tooltip("Ignore for now")] public bool play = false;
    private float _timeInLoop = 0f;
    [ShowInInspector, ReadOnly, ProgressBar(0f,1f)]
    public float progress { get { return Mathf.Clamp01(_timeInLoop / melody.TotalSeconds); } }

    [Button]
    void PlayOnce()
    {
        foreach(var t in melody.tracks)
        {
            StartCoroutine(PlayLoop(t));
        }
    }


    IEnumerator PlayLoop(Track track, float startTime = 0f)
    {
        float duration = melody.TotalSeconds;
        Debug.Log($"Playing track {track.note} for {duration} seconds");
        float t = startTime;
        Debug.Log($"{track.beats.Count} BEATS IN {track.note}");
        Queue<float> notes = new Queue<float>(track.beats);
        float nextNote = notes.Count> 0 ? notes.Dequeue() : -1f;
        while (t < duration)
        {
            if (nextNote != -1f)
            {
                if (t >= nextNote)
                {
                    AudioSource source = AudioSourcePool.instance.GetAudioSource();
                    UseAudioSource(track, source);
                    nextNote = notes.Count > 0 ? notes.Dequeue() : -1f;
                    Debug.Log($"NEXT NOTE {track.note}");
                }
            } else
            {
                Debug.Log($"NO MORE NOTE {track.note}");
            }
            t += Time.deltaTime;
            yield return null;
            
        }
        Debug.Log("Loop Complete");
    }

    async void UseAudioSource(Track track, AudioSource source)
    {
        source.clip = track.note.cookingNote;
        source.pitch = track.pitch;
        source.Play();
        // check for null in case we stop or quit while playing
        await UniTask.Delay((int)(track.note.cookingNote.length * 1000));
        if (source != null) AudioSourcePool.instance.ReleaseAudioSource(source);
    }
}
