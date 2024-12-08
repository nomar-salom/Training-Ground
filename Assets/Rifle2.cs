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

    public AmmoCounter ammoText;

    void Start()
    {
        _input = transform.root.GetComponent<PlayerMovement>();
        bulletsLeft = magazineSize;
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

        if (bulletsLeft <= 0) StartCoroutine(Reload());
                       
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate;
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);
        bulletsLeft--;
        ammoText.UseRifleAmmo();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        isReloading = false;
        ammoText.reloadRifleAmmoText();
    }
}
