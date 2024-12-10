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
    [SerializeField] private float fireRate = 0.0f;

    private int bulletsLeft;
    private bool isReloading = false;
    private float nextFireTime = 0f;

    private bool lastShootState = false;

    public AudioClip shootingSound; // Shooting sound
    // [SerializeField] private AudioClip reloadingSound; // Reloading sound
    public AudioSource audioSource;

    public AmmoCounter ammoText;
    public AudioSource reloadSound;



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
        if (isReloading) return;

        // Fire only when the input state changes (from not shooting to shooting)
        if (_input.shoot && !lastShootState && bulletsLeft > 0 && Time.time >= nextFireTime)
        {
            Shoot();
        }

        lastShootState = _input.shoot; // Track the previous frame's input state

        if (bulletsLeft <= 0) 
        {
            StartCoroutine(Reload());
            // bulletsLeft = magazineSize;
            // isReloading = false;
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate;
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);
        bulletsLeft--;

        // Play the shooting sound
        if (audioSource != null && shootingSound != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
        ammoText.UseRifleAmmo();
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
        ammoText.reloadRifleAmmoText();

    }
}
