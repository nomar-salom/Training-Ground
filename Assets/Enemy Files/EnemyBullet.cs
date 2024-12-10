using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float lifeTime = 5f;  // Time before the bullet is destroyed

    void Start()
    {
        // Destroy the bullet after a certain time
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Add damage logic to the player here
            Debug.Log("Player hit!");
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}

