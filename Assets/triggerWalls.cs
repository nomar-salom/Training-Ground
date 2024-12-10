using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class triggerWalls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            tag = "ActiveWall";
        }
    }
}
