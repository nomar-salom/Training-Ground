using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun2 : MonoBehaviour
{
    private PlayerMovement _input; // Replace "PlayerMovement" with your actual player controller class name
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 600f;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 0.1f; // Time (in seconds) between consecutive shots

    public AmmoCounter ammoText;
    public AudioSource reloadSound;


    private int bulletsLeft;
    // private bool isReloading = false;
    public bool isReloading;
    private float nextFireTime = 0f;

    		
    public AudioClip shootingSound; // Shooting sound
    // [SerializeField] private AudioClip reloadingSound; // Reloading sound
    public AudioSource audioSource;

    void Start()
    {
        _input = transform.root.GetComponent<PlayerMovement>(); // Replace with your player controller class
        bulletsLeft = magazineSize;
        isReloading = false;

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
        // If the rifle is not active, do nothing
        if (!gameObject.activeSelf) return;

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
        Debug.Log("shoot smg");
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);
        bulletsLeft--;

        // Play the shooting sound
        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
        ammoText.UseSubMachinegunAmmo();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        ammoText.ShowReloading();
        if (reloadSound != null)
        {
            reloadSound.time = reloadSound.clip.length - 2f; //the first two seconds of the soundfile is silent
            reloadSound.Play();
        }
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        isReloading = false;
        // Play the reloading sound
        /*if (audioSource != null && reloadingSound != null)
        {
            audioSource.PlayOneShot(reloadingSound);
        }*/

        ammoText.HideReloading();
        ammoText.reloadSubMachinegunAmmoText();

    }
}
