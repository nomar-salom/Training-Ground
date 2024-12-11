using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;       
    private CharacterController controller;
    public float gravity = -9.8f; 
    private Vector3 velocity;
    public float jumpHeight = 5f;
    private bool groundBelowPlayer;
    public Camera playerCamera;

    public float horizontalLimit = 60f; 
    //camera sensitivity
    public float sensitivity = 5f;
    //Vertical change in camera
    private float rotationX = 0f; 
    public bool shoot;

    public GameObject rifle;
    public GameObject smg;
    private bool isRifleActive = true;
    public Rifle2 rifleScript;
    public MachineGun2 smgScript;

    public AmmoCounter ammoCounter;

    public  PlayerHealth playerHealth;

    float timeSinceDamage = 0;

    public AudioClip ouchSound; // Shooting sound
    // [SerializeField] private AudioClip reloadingSound; // Reloading sound
    public AudioSource audioSource;

    void Start()
    {
        rifleScript = rifle.GetComponentInChildren<Rifle2>();
        smgScript = smg.GetComponentInChildren<MachineGun2>();
        rifle.SetActive(true);
        smg.SetActive(false);

        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {   
        groundBelowPlayer = controller.isGrounded;
        //Adjusts vertical velocity  when there is ground beneath player (stops bouncing)
        if(groundBelowPlayer && velocity.y < 0) {
            velocity.y = -2f;
        }

        //Gets movement if moving left or right (A or D keys)
        float moveLeftRight = Input.GetAxis("Horizontal"); 

        //Gets movement if moving forward or backward (W or S keys)
        float moveForwardBackward = Input.GetAxis("Vertical");

        //calculates direction of player based on input
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 move = right * moveLeftRight + forward * moveForwardBackward;

        //calculates how player moves based on input
        Vector3 amtOfMovement = move * speed * Time.deltaTime;
        controller.Move(amtOfMovement);   

        if(groundBelowPlayer && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("We're jumping and popping");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(!groundBelowPlayer) {
            velocity.y = velocity.y + (gravity * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);

        //handles camera movements based on inputs and sensitivity
        if(playerCamera != null) {
            float mouseVerticalInput = Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = rotationX - mouseVerticalInput;
            //clamping helps in avoiding the camera flipping like crazy
            rotationX = Mathf.Clamp(rotationX, -horizontalLimit, horizontalLimit); 
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            //horizontal rotation taking into account input/sensitivity
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity);
        }
<<<<<<< Updated upstream
        shoot = Input.GetMouseButtonDown(0);    
=======

        


        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon();
        }
        
        shoot = Input.GetMouseButton(0);    
>>>>>>> Stashed changes
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ouch") && Time.time - timeSinceDamage > 2) {
            Debug.Log("TAKING DAMAGE! He got hit by something tagged 'ouch' ");
            playerHealth.TakeDamage(1);
            audioSource.PlayOneShot(ouchSound);
            timeSinceDamage = Time.time;
        }
    }

    // void SwitchWeapon()
    // {
    //     Debug.Log("Switching weapon");
    //     isRifleActive = !isRifleActive;
    //     rifle.SetActive(isRifleActive);
    //     smg.SetActive(!isRifleActive);
    // }

    void SwitchWeapon()
    {
        /*if (rifleScript.isReloading || smgScript.isReloading)
        {
            Debug.Log("Cannot switch weapons while reloading.");
            return;
        }*/

        Debug.Log("Switching weapon");
        if (isRifleActive)
        {
            StartCoroutine(SwitchWeaponSmooth(rifle, smg));
            ammoCounter.UpdateAmmoUISubMachinegun();
        }
        else
        {
            StartCoroutine(SwitchWeaponSmooth(smg, rifle));
            ammoCounter.UpdateAmmoUIRifle();
        }
        isRifleActive = !isRifleActive;
    }


    IEnumerator SwitchWeaponSmooth(GameObject oldWeapon, GameObject newWeapon)
    {
        float duration = 0.3f; // Duration of the animation
        Vector3 initialOldPos = oldWeapon.transform.localPosition;
        Vector3 targetOldPos = initialOldPos + new Vector3(0, -0.5f, -0.5f); // Move backwards
        Vector3 initialNewPos = newWeapon.transform.localPosition;
        Vector3 targetNewPos = initialNewPos + new Vector3(0, 0.5f, 0.5f); // Move forwards

        // Deactivate shooting while switching weapons
        shoot = false;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            oldWeapon.transform.localPosition = Vector3.Lerp(initialOldPos, targetOldPos, t / duration);
            newWeapon.transform.localPosition = Vector3.Lerp(initialNewPos, targetNewPos, t / duration);
            yield return null;
        }

        // Activate/deactivate weapons
        oldWeapon.SetActive(false);
        newWeapon.SetActive(true);
    }
    
}
