using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Rigidbody rb;
    //private float lifeTime = 2f;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void Update()
    {
        //  transform.rotation = Quaternion.LookRotation(rb.velocity);
        SpinArrow();
        Destroy(gameObject, 20f);
    }


    private void SpinArrow()
    {
        float yVelocity = rb.velocity.y;
        float zVelocity = rb.velocity.z;
        float xVelocity = rb.velocity.x;
        float combinedVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);
        float fallAngle = -1 * Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
         rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            this.gameObject.GetComponent<Arrow>().enabled = false;

    }
}
       
