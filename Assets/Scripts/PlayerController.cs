using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;

    [Header("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float crouchSpeed;
    public float runSpeed = 10f;

    [Header("Rotation")]
    public float rotationSensibility = 10f;

    [Header("Jump")]
    public float jumpHeight = 1.9f;

    [Header("Crouch")]
    public float standingHeight = 1.8f;
    public float crouchingHeight = 1.25f;

    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationinput = Vector3.zero;
    CharacterController characterController;
    Animator animator;
    float xRotation = 0f;
    float YRotation = 0f;

    [Header("RingMenu")]
    public RingMenu menuPrefab;
    protected RingMenu menuInstanced;

    [HideInInspector]
    public ControllerMode Mode;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SetMode(ControllerMode.Play);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
        Move();
        GetAnim();
        GetRadialMenu();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);


            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                characterController.height = crouchingHeight;
                moveInput = transform.TransformDirection(moveInput) * crouchSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
                characterController.height = standingHeight;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        if (Interactor.isInteraction == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

            //control rotation around x axis (Look up and down)
            xRotation -= mouseY;

            //we clamp the rotation so we cant Over-rotate (like in real life)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //control rotation around y axis (Look up and down)
            YRotation += mouseX;


            //applying both rotations
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }

    private void GetAnim()
    {
        if (moveInput.x == 0)
        {
            animator.SetFloat("Speed", 0f);
        }
        else if (moveInput != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 0.5f);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("Speed", 1f);
        }
    }

    private void GetRadialMenu()
    {
        if (Mode == ControllerMode.Play)
        {
            if (Input.GetMouseButtonDown(2))
            {
                SetMode(ControllerMode.Menu);
                menuInstanced = Instantiate(menuPrefab, FindObjectOfType<Canvas>().transform);
                menuInstanced.callback = MenuClick;
            }
        }

    }

    private void MenuClick(string path)
    {

        Debug.Log("Instanciamos el objeto");
        Interactor.isInteraction = false;
        SetMode(ControllerMode.Play);
        //Debug.Log(path);
        //var paths = path.Split('/');
        //GetComponent<>().SetPrefab(int.Parse(paths[1]), int.Parse(paths[2]));
        //SetMode(ControllerMode.Build);
    }

    public enum ControllerMode
    {
        Play,
        Build,
        Menu
    }

    public void SetMode(ControllerMode mode)
    {
        Mode = mode;
        if (mode != ControllerMode.Menu && menuInstanced != null)
            Destroy(menuInstanced);

        switch (mode)
        {
            case ControllerMode.Build:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case ControllerMode.Menu:
                Interactor.isInteraction = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case ControllerMode.Play:
                Interactor.isInteraction = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
        }
    }

}