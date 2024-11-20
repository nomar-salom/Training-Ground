using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PlayerController; //uncomment later and replace "PlayerController" with name of playercontroller class


/*
Add this to playert controller:

public bool shoot;

public void OnSprint(InputValue value)
{
    shoot = value.isPressed;
}
*/

public class Rifle2 : MonoBehaviour
{
    bool test; //placeholder for value.isPressed
    //private PlayerController _input; //uncomment later and replace "PlayerController" with name of playercontroller class
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private float bulletSpeed = 600;
    // Start is called before the first frame update
    void Start()
    {
        //_input = transform.root.GetComponent<PlayerController>() //uncommnet later and replace "PlayerController" with name of playercontroller class
    }

    // Update is called once per frame
    void Update()
    {
        if (test) // replace with "test" with "_input.shoot"
        {
            Shoot();
            test = false; // replace with "test" with "_input.shoot"
        }
    }

    void Shoot()
    {
        Debug.Log("shoot");
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);
    }
}
