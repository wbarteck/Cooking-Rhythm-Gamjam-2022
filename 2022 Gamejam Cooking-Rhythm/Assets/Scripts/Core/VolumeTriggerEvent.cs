using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class VolumeTriggerEvent : MonoBehaviour
{
    public GameEvent gameEventtoTrigger;

    // hide alpha selector
    [ColorUsage(false)]
    public Color volumeColor = Color.yellow;
    // property with correct alpha
    private Color VolumeColor { get { return new Color(volumeColor.r, volumeColor.g, volumeColor.b, alpha); } }
    private float alpha = 0.2f;


    private void OnDrawGizmos()
    {
        Gizmos.color = VolumeColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        // draws a cube the same size as the gameObjects scale
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Customer>(out Customer customer)) {
            // ?. does if (obj != null) then call the function.
            // if (gameEventtoTrigger != null) gameEventtoTrigger.Raise();
            gameEventtoTrigger?.Raise();
        }
        
    }

}
