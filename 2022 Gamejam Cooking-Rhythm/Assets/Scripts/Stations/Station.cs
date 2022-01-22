using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class Station : MonoBehaviour
{
    [SerializeField] LoopingPlayer playhead;

    public Track track;

    private bool isPlaying;

    [Button] public void ClearNotes() { 
        track.beats.Clear(); 
        playhead?.UpdateBeat(); 
    }

    public void ResetNotes()
    {
        track.beats.Clear(); 
    }

    public static List<TimedNote> PlayerNotes()
    {
        // collect all station gameobjects
        var allStations = FindObjectsOfType<Station>();
        return Track.TracksToTimedNotes(allStations.Select(station => station.track).ToArray(), true);
    }

    public void ActivateStation() => PlayerCanvasManager.instance.ActivateStation(this);
    public void DeactivateStation() => PlayerCanvasManager.instance.LeaveStation(this);

    [Button] public void AddBeat()
    {
        float currentTime = playhead.GetCurrentTime;
        track.beats.Add(currentTime);

        // play note
        PlayOneShot();

        // when we add beat, update the playhead
        playhead.UpdateBeat();
    }

    async void PlayOneShot()
    {
        isPlaying = true;
        AudioSource source = AudioSourcePool.instance.GetAudioSource();
        source.clip = track.note.cookingNote;
        source.pitch = track.pitch;
        source.volume = track.note.volume;
        source.Play();
        // release audio source to pool ocne the sound is finished playing
        await UniTask.Delay((int)(track.note.cookingNote.length * 1000));
        if (source != null) AudioSourcePool.instance.ReleaseAudioSource(source);
        isPlaying = false;
    }

    public void AdjustPitch(float _pitch)
    {
        track.pitch = _pitch;
        playhead.UpdateBeat();
    }

    private void OnMouseEnter()
    {
        if (isPlaying) return;
        PlayOneShot();
    }
}
