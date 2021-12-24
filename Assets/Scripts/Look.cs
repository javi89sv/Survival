using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public static bool cursorLocked;

    public Transform player;
    public Transform cams;
    public Transform weapon;

    public float xSensitivity;
    public float ySensitivity;
    public float maxAngle;

    Quaternion camCenter;


    void Start()
    {
        camCenter = cams.localRotation;
    }


    void Update()
    {

        SetY();
        SetX();

        UpdateCursorLock();
    }

    void SetY()
    {
        float input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, -Vector3.right);
        Quaternion delta = cams.localRotation * adj;

        if (Quaternion.Angle(camCenter, delta) < maxAngle)
        {
            cams.localRotation = delta;
            weapon.localRotation = delta;
        }
        weapon.rotation = cams.rotation;
    }
    void SetX()
    {
        float input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, Vector3.up);
        Quaternion delta = player.localRotation * adj;
        player.localRotation = delta;
    }

    void UpdateCursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }
}
