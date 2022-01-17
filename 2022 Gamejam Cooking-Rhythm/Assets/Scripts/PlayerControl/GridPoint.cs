using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    public GameEvent enterEvent;
    public GameEvent exitEvent;

    [SerializeField] BoxCollider collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            enterEvent?.Raise();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            exitEvent?.Raise();
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
