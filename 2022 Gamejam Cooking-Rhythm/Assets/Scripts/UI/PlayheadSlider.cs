using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayheadSlider : MonoBehaviour
{
    [SerializeField] LoopingPlayer playhead;
    [SerializeField] Slider slider;

   
    // Update is called once per frame
    void Update()
    {
        slider.value = playhead.Progress;
    }
}
