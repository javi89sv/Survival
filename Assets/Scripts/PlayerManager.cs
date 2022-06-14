using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

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


    
    public float currentHealth, currentHungry, currentThirst;
    [Header("--Player Stats--")]
    public float maxHealth;
    public float maxHungry, maxThirst;
    public float healthIncreaseRate, drinkIncreaseRate, foodIncreaseRate;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        currentHealth = maxHealth;
        currentHungry = maxHungry;
        currentThirst = maxThirst;

        baseFOV = normalCam.fieldOfView;

    }

    private void Update()
    {
        ManagerStatesPlayer();
    }
    private void FixedUpdate()
    {

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

        //Recoge Items

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceInteract))
        {

            if (hit.collider.GetComponent<ItemPickUp>())
            {
                InfoUI.Instance.SetTooltipItem(hit.collider.name);

                var itemPickUp = hit.collider.GetComponent<ItemPickUp>();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    var inventory = GetComponent<PlayerInventoryHolder>();

                    if (!inventory)
                    {
                        return;
                    }

                    if (inventory.AddToInventory(itemPickUp.item, itemPickUp.amount))
                    {
                        Destroy(itemPickUp.gameObject);
                    }
                }
            }

        }
        else
        {
            InfoUI.Instance.HideText();
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
        currentHungry -= foodIncreaseRate * Time.deltaTime;
        currentThirst -= drinkIncreaseRate * Time.deltaTime;

        if (currentHungry <= 0 || currentThirst < 0)
        {
            currentHealth -= healthIncreaseRate * Time.deltaTime;
        }
    }

}
