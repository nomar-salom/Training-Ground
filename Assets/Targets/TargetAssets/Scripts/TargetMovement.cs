using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement: MonoBehaviour
{
    public float speed = 3f;  // Movement speed
    public float range = 3f; // How far it moves from the center
    public bool isMoving = false;

    private Vector3 startPos;
    private bool isPaused = false;

    bool firstTime = true;

    float startTime;

    float deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving && firstTime)
        {
            startTime = Time.time;
            firstTime = false;
        }
        if (isMoving)
        {
            //this moves left to right
            deltaTime = Time.time - startTime;
            float offset = Mathf.Sin((deltaTime) * speed) * range;
            transform.position = startPos + new Vector3(offset, 0, 0);
        }

    }
}
