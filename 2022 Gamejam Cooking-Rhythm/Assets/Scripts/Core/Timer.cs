using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
public class Timer : MonoBehaviour
{
    public static UnityEvent timerFinishedAction;

    [SerializeField] bool useTextMeshPro = true;
    [SerializeField] float timeLeft = 60f;

    [SerializeField, ShowIf("useTextMeshPro")] TMP_Text tmp_text;
    [SerializeField, HideIf("useTextMeshPro")] Text ui_text;

    public void SetTime(float seconds, bool start = false)
    {
        timeLeft = seconds;
        if (start) StartTimer();
    }
    [ButtonGroup("TimerControls")] public void StartTimer() => StartCoroutine(TimerRoutine());
    [ButtonGroup("TimerControls")] public void StopTimer() => StopAllCoroutines();
    IEnumerator TimerRoutine()
    {
        while(timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            UpdateText();
            yield return null;
        }

        timerFinishedAction?.Invoke();
    }

    void UpdateText()
    {
        string time = TimeSpan.FromSeconds(timeLeft).ToString(@"mm\:ss");
        if (useTextMeshPro && tmp_text != null)
        {
            tmp_text.text = time;
        } else if(ui_text != null)
        {
            ui_text.text = time;
        }
    }
}
