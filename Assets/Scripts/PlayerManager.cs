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
    public float currentVelY = 0;

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
    }

    public void CheckIsGrounded()
    {
        Collider[] cols = Physics.OverlapSphere(groundDetectionTransform.position, 0.5f, groundMask);

        if (cols.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        CheckIsGrounded();

        if (isGrounded == false)
        {
            currentVelY += gravityFactor * Time.deltaTime;
        }
        else if (isGrounded == true)
        {
            currentVelY = -2f;
        }

        if (Input.GetKeyDown("space") && isGrounded == true)
        {
            currentVelY = jumpPower;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        Vector3 movement = new Vector3();

        movement = inputX * transform.right + inputY * transform.forward;

        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            movement *= crouchingMulitplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if (isSprinting == true)
        {
            movement *= sprintingMultiplier;
        }

        controller.Move(movement * movementSpeed * Time.deltaTime);
        controller.Move(new Vector3(0, currentVelY * Time.deltaTime, 0));

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
