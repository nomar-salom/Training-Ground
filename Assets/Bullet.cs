using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object hit belongs to the "Default" layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
