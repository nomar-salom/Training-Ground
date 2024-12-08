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
    

    void Start()
    {
        currentAmmoRifle = maxAmmoRifle;
        currentAmmoSubMachinegun = maxAmmoSubMachinegun;
        UpdateAmmoUIRifle();

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
   // public void UseSubMachinegunAmmo()
   // {
   //     if (currentAmmoSubMachinegun > 0)
    //    {
    //        currentAmmoSubMachinegun--;
   //         UpdateAmmoUISubMachinegun();
   //     }
    //}

    void UpdateAmmoUIRifle()
    {
        ammoText.text = "Ammo: " + currentAmmoRifle + " / " + maxAmmoRifle;
    }
    //void UpdateAmmoUISubMachinegun()
    //{
    //    ammoText.text = $"Ammo: " + currentAmmoSubMachinegun + " / " + maxAmmoSubMachinegun;
    //}
}
