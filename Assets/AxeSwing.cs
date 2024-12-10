using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public float speed = 2f;  // Movement speed

    private Quaternion startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.rotation;
    }

    void Update()
    {
        float angle = ((Mathf.Sin(Time.time * speed)-1) * 55);
        // transform.rotation = new Quaternion.Euler(0, 0, 90, angle);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle - 125);
    }
}
