using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public int health = 1;
    public float rotationSpeed = 2f;
    public bool isRotating = false;
    public bool targetHit = false;
    private float targetRotationX;
    private TargetMovement targetMovement; // Reference to TargetMovement
    public bool onWall = false;//Used for the wall targets

    public bool flipDirection = false;

    void Start()
    {
        // Get the TargetMovement script attached to the same GameObject
        targetMovement = GetComponent<TargetMovement>();
    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has a "Projectile" tag
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 1; // Reduce health
            Debug.Log("Target hit!");

            // Destroy the projectile
            Destroy(collision.gameObject);
            if (targetMovement != null)
            {
                targetMovement.isMoving = false;
            }
            // When the target is hit, knock it down 90 degrees
            if (!isRotating && !targetHit && !onWall)
            {
                targetHit = true;
                targetRotationX = transform.rotation.eulerAngles.x + 90f; // Increase rotation by 90 degrees
                StartCoroutine(RotateTarget());
            }            
            
            if (!isRotating && !targetHit && onWall)
            {
                targetHit = true;
                targetRotationX = transform.rotation.eulerAngles.y - 90f; // Increase rotation by 90 degrees
                StartCoroutine(RotateTarget());
            }
        }
    }

    // Function for smooth rotation after the target is hit
    private System.Collections.IEnumerator RotateTarget()
    {
        isRotating = true;

        Quaternion finalRotation;

        // Cache the initial rotation
        Quaternion initialRotation = transform.rotation;
        if(!onWall)
        {
            finalRotation = Quaternion.Euler(targetRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        else
        {
            if(flipDirection)
            {
                finalRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationX, transform.rotation.eulerAngles.z);
            }
            else
            {
                finalRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -1 * targetRotationX, transform.rotation.eulerAngles.z);
            }
        }

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            // Smoothly interpolate between initial and final rotation
            transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed; // Adjust by rotation speed
            yield return null; // Wait for the next frame
        }

        // Snap to the final rotation to avoid any small discrepancies
        transform.rotation = finalRotation;

        isRotating = false;
    }
}
