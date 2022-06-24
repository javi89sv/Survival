using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public float movementSpeed = 1f;
    public float jumpPower = 10f;

    public float gravityFactor = -9.81f;


    public bool isSprinting = false;
    public float sprintingMultiplier;

    public bool isCrouching = false;
    public float crouchingMulitplier;

    public CharacterController controller;
    public float standingHeight = 1.8f;
    public float crouchingHeight = 1.25f;

    public LayerMask groundMask;
    public Transform groundDetectionTransform;

    public bool isGrounded;

    Vector3 velocity;

    [HideInInspector]
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

        //  baseFOV = normalCam.fieldOfView;

    }

    private void Update()
    {
        ManagerStatesPlayer();

        isGrounded = Physics.CheckSphere(groundDetectionTransform.position, 0.3f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 move = transform.right * inputX + transform.forward * inputY;

        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            move *= crouchingMulitplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravityFactor);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded == true)
        {
            isSprinting = true;
            move *= sprintingMultiplier;
        }
        else
        {
            isSprinting = false;
        }

        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravityFactor * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        ////Field of View
        //if (sprint)
        //{
        //    normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
        //    movementSpeed *= sprintModifier;
        //}
        //else
        //{
        //    normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);           
        //}
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
