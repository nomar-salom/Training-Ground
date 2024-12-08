using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
    public Timer timer;
    public PlayerScore score;
    public bool trigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !trigger)
        {
            trigger = true;
            timer.StopTimer();
            score.CheckHighScore();
        }
    }
}
