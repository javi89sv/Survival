using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks
{

    public float speed;
    public float sprintModifier;
    public float jumpForce;

    public Camera normalCam;
    public GameObject cameraParent;
    public LayerMask ground;
    public Transform groundDetector;
    public Transform weaponParent;

    private Rigidbody rig;
    private float baseFOV;
    private float sprintFOVModifier = 1.2f;
    private float movementCounter;
    private float idleCounter;

    private Vector3 weaponParentOrigin;
    private Vector3 targetWeaponBobPosition;

    
    [HideInInspector]
    public float currentHealth, currentFood, currentDrink;
    public float maxHealth, maxFood, maxDrink;
    public float healthIncreaseRate, drinkIncreaseRate, foodIncreaseRate;

    //UI
    private GameObject ui_healthbar;
    private GameObject ui_foodbar;
    private GameObject ui_drinkbar;

    private void Awake()
    {
        weaponParentOrigin = weaponParent.localPosition;
      
    }

    void Start()
    {

        currentHealth = maxHealth;
        currentFood = maxFood;
        currentDrink = maxDrink;
        if (photonView.IsMine)
        {
            ui_healthbar = GameObject.Find("StatusPlayer/HealthBar");
            ui_foodbar = GameObject.Find("StatusPlayer/FoodBar");
            ui_drinkbar = GameObject.Find("StatusPlayer/DrinkBar"); 
        }
       
       
        if (photonView.IsMine)
        {
            cameraParent.SetActive(true);
        }
        else
        {
            cameraParent.SetActive(false);
        }

        if (!photonView.IsMine)
        {
            gameObject.layer = 6;
        }

        baseFOV = normalCam.fieldOfView;
        rig = GetComponent<Rigidbody>();


    }
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        ManagerStatesPlayer();

        //Axies
        float hmove = Input.GetAxisRaw("Horizontal");
        float vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        //States

        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.05f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && vmove > 0 && !isJumping;

        //Jumping
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }

        //HeadBob
        if (hmove == 0 && vmove == 0)
        {
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2);
        }
        else if (!isSprinting)
        {
            HeadBob(movementCounter, 0.035f, 0.035f);
            movementCounter += Time.deltaTime * 3f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6);
        }
        else
        {
            HeadBob(movementCounter, 0.05f, 0.05f);
            movementCounter += Time.deltaTime * 7f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10);
        }


        //Movement
        Vector3 direction = new Vector3(hmove, 0, vmove);
        direction.Normalize();

        float adjustedSpeed = speed;

        if (isSprinting)
        {
            adjustedSpeed *= sprintModifier;
        }

        Vector3 targetVelocity = transform.TransformDirection(direction) * adjustedSpeed * Time.deltaTime;
        targetVelocity.y = rig.velocity.y;
        rig.velocity = targetVelocity;


        //Field of View
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);

        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
        }

    }

    private void FixedUpdate()
    {
       
    }

    private void HeadBob(float z, float x_intensity, float y_intensity)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z * 2) * x_intensity, Mathf.Sin(z * 2) * y_intensity, 0);
    }

    private void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);
            if (currentHealth < 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void ManagerStatesPlayer()
    {
        ui_healthbar.GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        ui_drinkbar.GetComponent<Image>().fillAmount = currentDrink / maxDrink;
        ui_foodbar.GetComponent<Image>().fillAmount = currentFood / maxFood;

        currentFood -= foodIncreaseRate * Time.deltaTime;
        currentDrink -= drinkIncreaseRate * Time.deltaTime;

        if (currentFood <= 0 || currentDrink < 0)
        {
            currentHealth -= healthIncreaseRate * Time.deltaTime;
        }
    }

}
