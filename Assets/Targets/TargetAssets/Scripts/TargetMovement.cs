using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement: MonoBehaviour
{
    public float speed = 2f;  // Movement speed
    public float range = 3f; // How far it moves from the center
    public bool isMoving = false;

    private Vector3 startPos;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            //this moves left to right
            float offset = Mathf.Sin(Time.time * speed) * range;
            transform.position = startPos + new Vector3(offset, 0, 0);
        }

    }
}
