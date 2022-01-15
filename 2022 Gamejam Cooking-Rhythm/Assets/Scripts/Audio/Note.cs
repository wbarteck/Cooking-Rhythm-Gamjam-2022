using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Audio/Note")]
public class Note : ScriptableObject
{
    public AudioClip cookingNote;
    public AudioClip musicalNote;
    [Range(-1f,1f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume = 1f;
}
