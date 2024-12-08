using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text bestTimerText;
    public int minutes;
    public int seconds;
    public float bestTime;
    public int bestMinutes;
    public int bestSeconds;
    private float elapsedTime = 0f;
    private bool isTiming = false;
    

    void Update()
    {
        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void StartTimer()
    {
        isTiming = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isTiming = false;
        CheckBestTime();
    }

    public void UpdateTimerUI()
    {
        minutes = Mathf.FloorToInt(elapsedTime / 60f);
        seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void CheckBestTime()
    {
        if(elapsedTime > bestTime)
        {
            bestTime = elapsedTime;
            bestMinutes = Mathf.FloorToInt(elapsedTime / 60f);
            bestSeconds = Mathf.FloorToInt(elapsedTime % 60f);
            bestTimerText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }
}
