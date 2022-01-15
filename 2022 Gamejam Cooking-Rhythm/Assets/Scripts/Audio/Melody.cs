using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities.Editor;
#endif

[CreateAssetMenu(menuName = "Audio/Melody")]
public class Melody : SerializedScriptableObject // could also just be a class
{
    public Track[] tracks;
    [OnValueChanged("UpdateTracks")]
    public int bpm = 103;
    [MinValue(0f), MaxValue(6f)]
    [OnValueChanged("UpdateTracks")]
    public int bars = 2;
    [ShowInInspector, ReadOnly] private float beatDuration { get { return 60f / bpm; } }
    private int TrackLength { get { return bars * 8; } }
    [ShowInInspector, ReadOnly] public float TotalSeconds { get { return beatDuration * 4 * bars; } }

    [Button("ResetTrackEditor"), DisableIf("@this.tracks.Length == 0")]
    void CreateTrackEditor()
    {
        
#if UNITY_EDITOR
        trackBeats = new bool[TrackLength, Mathf.Max(1,tracks.Length)];
#endif
    }

#if UNITY_EDITOR
    
    [TableMatrix(DrawElementMethod = "DrawCell", HideColumnIndices = true, HideRowIndices = true, HorizontalTitle = "Melody Editor")]
    [OnValueChanged("UpdateTracks")]
    [SerializeField]
    private bool[,] trackBeats = new bool[0,0];
    private bool DrawCell(Rect rect, bool value)
    {
        
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            value = !value;
            GUI.changed = true;
            Event.current.Use();
            
        }

        EditorGUI.DrawRect(
            rect,
            value ? new Color(.1f, .8f, .2f) : new Color(0, 0, 0, .5f)
            );

        return value;
    }

    private void UpdateTracks()
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            ref Track t = ref tracks[i];
            List<float> beats = new List<float>();
            List<float> durations = new List<float>();
            for (int j = 0; j < TrackLength; j++)
            {
                var beat = trackBeats[j, i];
                if (beat)
                {
                    float beatTiming = TotalSeconds * ((float)j / TrackLength);
                    beats.Add(beatTiming);
                    durations.Add(0); // FIXME later for sustained notes

                }
            }
            t.beats = beats;
        }
    }
#endif

    
}
