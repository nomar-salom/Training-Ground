using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    public int maxAmmoRifle = 8;
    public int currentAmmoRifle;
    public int maxAmmoSubMachinegun = 30;
    public int currentAmmoSubMachinegun;
    public TMP_Text ammoText;
    public TMP_Text reloadingText;
    

    void Start()
    {
        currentAmmoRifle = maxAmmoRifle;
        currentAmmoSubMachinegun = maxAmmoSubMachinegun;
        UpdateAmmoUIRifle();
        reloadingText.gameObject.SetActive(false);

    }

    public void reloadRifleAmmoText()
    {
        currentAmmoRifle = maxAmmoRifle;
        UpdateAmmoUIRifle();
    }

    public void UseRifleAmmo()
    {
        if (currentAmmoRifle > 0)
        {
            currentAmmoRifle--;
            UpdateAmmoUIRifle();
        }
    }

    void UpdateAmmoUIRifle()
    {
        ammoText.text = "Ammo: " + currentAmmoRifle + " / " + maxAmmoRifle;
    }

    public void ShowReloading()
    {
        reloadingText.gameObject.SetActive(true);
    }

    public void HideReloading()
    {
        reloadingText.gameObject.SetActive(false);
    }

    //FIXME: When Gunswap is finished, check to make sure this works. It'll need a flag of some sort to recognize which gun is currently active
    public void UseSubMachinegunAmmo()
    {
        if (currentAmmoSubMachinegun > 0)
       {
           currentAmmoSubMachinegun--;
            UpdateAmmoUISubMachinegun();
        }
    }

    void UpdateAmmoUISubMachinegun()
    {
       ammoText.text = $"Ammo: " + currentAmmoSubMachinegun + " / " + maxAmmoSubMachinegun;
    }
}
