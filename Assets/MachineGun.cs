using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    private PlayerMovement _input; // Replace "PlayerMovement" with your actual player controller class name
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 600f;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 0.1f; // Time (in seconds) between consecutive shots

    private int bulletsLeft;
    private bool isReloading = false;
    private float nextFireTime = 0f;

    
    public AudioClip shootingSound; // Shooting sound
    // [SerializeField] private AudioClip reloadingSound; // Reloading sound
    public AudioSource audioSource;

    public AmmoCounter ammoText;
    public AudioSource reloadSound;

    void Start()
    {
        _input = transform.root.GetComponent<PlayerMovement>(); // Replace with your player controller class
        bulletsLeft = magazineSize;
    }

    void Update()
    {
        if (isReloading)
            return;

        // Continuous shooting while button is held
        if (_input.shoot && bulletsLeft > 0 && Time.time >= nextFireTime)
        {
            Shoot();
        }

        // Auto-reload when out of bullets
        if (bulletsLeft <= 0)
        {
            StartCoroutine(Reload());
        }

        // Uncomment to allow manual reload using "R"
        /*
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize)
        {
            StartCoroutine(Reload());
        }
        */
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate; // Set the next allowed fire time
        Debug.Log("shoot");
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);
        bulletsLeft--;

        ammoText.UseSubMachinegunAmmo();

        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        ammoText.reloadSubMachinegunAmmoText();


        if (audioSource != null && reloadSound != null)
        {
            reloadSound.Play();
        }
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        isReloading = false;
        Debug.Log("Reload complete!");


    }
}
