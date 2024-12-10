using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimer : MonoBehaviour
{
    public Timer timer;
    public PlayerScore score;
    public bool trigger = false;

    public GameObject demon;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !trigger)
        {
            trigger = true;
            timer.StopTimer();
            score.CheckHighScore();
        }
    }*/

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(!trigger && demon.CompareTag("Dead"))
        {
            trigger = true;
            timer.StopTimer();
            score.CheckHighScore();
        }
    }
}
