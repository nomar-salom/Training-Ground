using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public int health = 1;
    public float rotationSpeed = 2f;
    public bool isRotating = true;
    private float targetRotationX;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a "Projectile" tag
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 1; // Reduce health
            Debug.Log("Target hit!");

            // Destroy the projectile
            Destroy(collision.gameObject);

            // When the target is hit, knock it down 90 degrees
            if (!isRotating)
            {
                targetRotationX = transform.rotation.eulerAngles.x - 90f; // Decrease rotation by 90 degrees
                StartCoroutine(RotateTarget());
            }
        }
    }

    //function for smooth rotation after the target is hit
    private System.Collections.IEnumerator RotateTarget()
    {
        isRotating = true;

        float currentRotationX = transform.rotation.eulerAngles.x;
        while (Mathf.Abs(currentRotationX - targetRotationX) > 0.1f)
        {
            // Smoothly interpolate to the target rotation
            currentRotationX = Mathf.Lerp(currentRotationX, targetRotationX, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(currentRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            yield return null; // Wait for the next frame
        }

        // Snap to the exact target rotation
        transform.rotation = Quaternion.Euler(targetRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        isRotating = false;
    }
}
