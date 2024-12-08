using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Timer timer;
    public bool trigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !trigger)
        {
            trigger = true;
            timer.StartTimer();
        }
    }
}
