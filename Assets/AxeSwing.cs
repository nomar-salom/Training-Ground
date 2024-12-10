using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public float speed = 2f;  // Movement speed

    private Quaternion startPos;

    // public AudioClip swingSound;
    public AudioSource audioSource;
    bool swingingPositive = false;



    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.rotation;

        // audioSource.clip = swingSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float angle = (Mathf.Sin(Time.time * speed)-1) * 55;
        // transform.rotation = new Quaternion.Euler(0, 0, 90, angle);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle - 125);

        // Debug.Log(angle);

        if(swingingPositive && angle < -105 || !swingingPositive && angle > -5)
        {
            audioSource.Play();
            // Debug.Log("Sound played");
            swingingPositive = !swingingPositive;
        }
    }

    void playSound()
    {
    }
}
