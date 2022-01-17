using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridPoint : MonoBehaviour
{
    public GameEvent enterEvent;
    public GameEvent exitEvent;
    // sometimes its easier to just use unity events and wire things up
    public UnityEvent enterUnityEvent;
    public UnityEvent exitUnityEvent;

    [SerializeField] BoxCollider collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            enterEvent?.Raise();
            enterUnityEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            exitEvent?.Raise();
            exitUnityEvent?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Color c = Color.green;
        c.a = 0.2f;
        Gizmos.color = c;
        Gizmos.matrix = transform.localToWorldMatrix;
        
        // draws a cube the same size as the gameObjects scale
        Gizmos.DrawCube(collider.center, collider.bounds.size);
    }
}
