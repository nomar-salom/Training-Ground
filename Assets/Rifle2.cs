using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle2 : MonoBehaviour
{
    private PlayerMovement _input;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 600;
    [SerializeField] private int magazineSize = 8;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 0.5f;

    [SerializeField] private AudioClip shootingSound; // Shooting sound
    [SerializeField] private AudioClip reloadingSound; // Reloading sound
    private AudioSource audioSource;

    private int bulletsLeft;
    public bool isReloading = false;
    private float nextFireTime = 0.4f;

    private bool lastShootState = false;

<<<<<<< Updated upstream
=======
    public AudioClip shootingSound; // Shooting sound
    // [SerializeField] private AudioClip reloadingSound; // Reloading sound
    public AudioSource audioSource;

    public AmmoCounter ammoText;
    public AudioSource reloadSound;

    float lastShotTime;



>>>>>>> Stashed changes
    void Start()
    {
        _input = transform.root.GetComponent<PlayerMovement>();
        bulletsLeft = magazineSize;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on the Rifle2 GameObject.");
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void Update()
    {

        if (!gameObject.activeSelf) return;
        if (isReloading) return;

        // Fire only when the input state changes (from not shooting to shooting)
        if (_input.shoot && !lastShootState && bulletsLeft > 0 && Time.time - lastShotTime >= nextFireTime)
        {
            lastShotTime = Time.time;
            Shoot();
        }

        lastShootState = _input.shoot; // Track the previous frame's input state

        // Reload if out of bullets
        if (bulletsLeft <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
<<<<<<< Updated upstream
        nextFireTime = Time.time + fireRate;

        // Instantiate and fire the bullet
=======
        // nextFireTime = Time.time + fireRate;
>>>>>>> Stashed changes
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);

        bulletsLeft--;

        // Play the shooting sound
        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        // Play the reloading sound
        if (audioSource != null && reloadingSound != null)
        {
            audioSource.PlayOneShot(reloadingSound);
        }

        yield return new WaitForSeconds(reloadTime);

        bulletsLeft = magazineSize;
        isReloading = false;
    }
}
