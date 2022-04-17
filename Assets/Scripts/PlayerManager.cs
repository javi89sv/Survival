using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("--Movement--")]
    public CharacterController controller;
    public float speed;
    public float sprintModifier;
    public float jumpHeight;
    public float gravity = -9.81f * 2;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Transform groundCheck;
    private bool isGrounded;

    private Vector3 velocity;

    public Camera normalCam;
    public GameObject cameraParent;


    public Transform weaponParent;

    private float baseFOV;
    private float sprintFOVModifier = 1.2f;
    private float movementCounter;
    private float idleCounter;

    private Vector3 weaponParentOrigin;
    private Vector3 targetWeaponBobPosition;


    [HideInInspector]
    public float currentHealth, currentFood, currentDrink;
    [Header("--Player Stats--")]
    public float maxHealth;
    public float maxFood, maxDrink;
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

        ui_healthbar = GameObject.Find("StatusPlayer/HealthBar");
        ui_foodbar = GameObject.Find("StatusPlayer/FoodBar");
        ui_drinkbar = GameObject.Find("StatusPlayer/DrinkBar");


        baseFOV = normalCam.fieldOfView;



    }
    private void FixedUpdate()
    {


        ManagerStatesPlayer();

        //Axies
        float hmove = Input.GetAxisRaw("Horizontal");
        float vmove = Input.GetAxisRaw("Vertical");

        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetButtonDown("Jump");

        //States


        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && vmove > 0 && !isJumping;


        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }

        ////HeadBob
        //if (hmove == 0 && vmove == 0)
        //{
        //    HeadBob(idleCounter, 0.025f, 0.025f);
        //    idleCounter += Time.deltaTime;
        //    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2);
        //}
        //else if (!isSprinting)
        //{
        //    HeadBob(movementCounter, 0.035f, 0.035f);
        //    movementCounter += Time.deltaTime * 3f;
        //    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6);
        //}
        //else
        //{
        //    HeadBob(movementCounter, 0.05f, 0.05f);
        //    movementCounter += Time.deltaTime * 7f;
        //    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10);
        //}


        //MOVEMENT

        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


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


    private void HeadBob(float z, float x_intensity, float y_intensity)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z * 2) * x_intensity, Mathf.Sin(z * 2) * y_intensity, 0);
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth < 0)
        {
            Destroy(gameObject);
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
