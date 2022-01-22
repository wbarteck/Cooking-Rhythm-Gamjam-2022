using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    public CinemachineController cinemachineController;

    public float introTime = 1.0f;
    public void Awake()
    {
        //Run co routine and then switch camera index

    }

    IEnumerator IntroCamera()
    {
        yield return new WaitForSeconds(introTime);



        yield return null;
    }
}
