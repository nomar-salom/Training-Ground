using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatforms : MonoBehaviour
{
    public float speed = 2f;  // Movement speed

    private Vector3 startPos;

    public bool isOffset = false;
    float offset;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(isOffset)
        {
            offset = Mathf.Sin(Time.time * speed) * -2;
        }
        else
        {
            offset = Mathf.Sin(Time.time * -1 * speed) * -2;
        }
        // transform.rotation = new Quaternion.Euler(0, 0, 90, angle);
        transform.position = startPos + new Vector3(0, offset, 0);

    }
}
