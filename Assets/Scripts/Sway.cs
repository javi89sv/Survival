using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sway : MonoBehaviourPunCallbacks
{

    public float intensity;
    public float smooth;

    private Quaternion origin_rotation;

    private void Start()
    {
        origin_rotation = transform.localRotation;
    }

    public void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        UpdateSway();
    }

    private void UpdateSway()
    {
        //controls
        float x_mouse = Input.GetAxis("Mouse X");
        float y_mouse = Input.GetAxis("Mouse Y");

        //calculate target rotation
        Quaternion x_adj = Quaternion.AngleAxis(-intensity * x_mouse, Vector3.up);
        Quaternion y_adj = Quaternion.AngleAxis(intensity * y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * x_adj * y_adj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);


    }

}
