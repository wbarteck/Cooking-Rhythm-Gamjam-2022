using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayEvent : MonoBehaviour
{
    public GameEvent testEvent;

    [Button] public void TestEvent() { testEvent.Raise(); }
}
