using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestinationOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<PlayerMovement>().MoveHere(transform);
    }
}
