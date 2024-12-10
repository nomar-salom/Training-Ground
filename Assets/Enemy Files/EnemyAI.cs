using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject bulletPrefab;

    public float sightRange, attackRange, walkRange;
    public bool playerInSightRange, playerInAttackRange, playerInWalkRange;

    public bool canMove = true; // Toggle for whether the enemy can move
    public float moveSpeed = 3f; // Speed of movement towards the player

    private Animator animator; // Reference to the Animator
    private bool isMoving = false; // Tracks whether the enemy is currently moving
    private bool isDead = false; // Tracks whether the enemy is dead

    public AudioClip shootingSound; // The sound played when shooting
    public AudioClip deathSound; // The sound played when dying
    public AudioSource audioSource; // The audio source for playing sounds

    public AudioSource boom;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        if (audioSource.isPlaying)
        {
        audioSource.Stop();
        }
    }

    private void Start()
    {
        if (canMove && !isDead)
        {
            StartCoroutine(MoveCycle());
        }
    }

    private void Update()
{
    // Check ranges
    playerInSightRange = IsPlayerInSight();
    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    playerInWalkRange = Physics.CheckSphere(transform.position, walkRange, whatIsPlayer);

    if (isDead)
        return; // Do not process movement or attack if the enemy is dead

    // Handle movement only if the player is in walk range but not in attack range
    if (playerInWalkRange && !playerInAttackRange && canMove)
    {
        isMoving = true;
    }
    else
    {
        isMoving = false;
    }

    if (isMoving)
    {
        animator.SetBool("isWalking", true); // Play walking animation
        MoveTowardsPlayer();
    }
    else
    {
        animator.SetBool("isWalking", false); // Stop walking animation
    }

    // Handle attacking only if not moving and the player is in attack range and visible
    if (!isMoving && playerInAttackRange && playerInSightRange)
    {
        AttackPlayer();
    }
    else
    {
        animator.SetBool("isShooting", false); // Reset to idle if not attacking
    }
}

private bool IsPlayerInSight()
{
    Vector3 directionToPlayer = player.position - transform.position;
    RaycastHit hit;

    // Perform a raycast from the enemy to the player
    if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, sightRange, whatIsPlayer))
    {
        // If the raycast hits the player, return true
        if (hit.collider.CompareTag("Player"))
        {
            return true;
        }
    }

    // Return false if no player is detected in line of sight
    return false;
}


    private void MoveTowardsPlayer()
    {
        // Calculate direction while ignoring the height difference
        Vector3 direction = (new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position).normalized;

        // Move towards the player
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private IEnumerator MoveCycle()
    {
        while (true)
        {
            // Move for 4 seconds
            isMoving = true;
            yield return new WaitForSeconds(4f);

            // Stop for 4 seconds
            isMoving = false;
            yield return new WaitForSeconds(4f);
        }
    }

    private void AttackPlayer()
    {
        Vector3 targetLocation = player.transform.position;
        targetLocation = targetLocation - new Vector3 (0,.6f,0);
        transform.LookAt(targetLocation);

        if (!alreadyAttacked)
        {


            // Play shooting animation
            animator.SetBool("isShooting", true);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(rb.transform.forward * bulletSpeed);

            if (audioSource != null && shootingSound != null)
            {
                audioSource.PlayOneShot(shootingSound);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        animator.SetBool("isShooting", false); // Return to idle after shooting
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        // Play death animation
        animator.SetBool("isDead", true);
        isDead = true; // Set the enemy as dead


        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Invoke(nameof(DestroyEnemy), 4f); // Delay to let animation play


        tag = "Dead";
        
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkRange); // Walk range visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Attack range visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Sight range visualization
    }

    // Detect when the enemy is hit by a bullet (tagged "Bullet")
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }
            Destroy(other.gameObject); // Destroy the bullet
        }
    }
}
