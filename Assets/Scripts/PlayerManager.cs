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

    public Transform weaponParent;

    private float baseFOV;
    private float sprintFOVModifier = 1.2f;

    private float distanceInteract = 2f;


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

        //Inputs

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceInteract))
        {
            if (hit.collider.tag == "Storage")
            {
                ItemContainer chest = hit.transform.GetComponent<ItemContainer>();

                if (Input.GetKeyDown(KeyCode.E) && chest.isOpen != true)
                {
                    chest.isOpen = true;
                    chest.menuUI.gameObject.SetActive(true);
                    chest.OpenContainer();
                    GameManager.instance.inventoryEnable = true;
                }   
            }
        }
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
