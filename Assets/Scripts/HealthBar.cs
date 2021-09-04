using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameObject image;

    GameObject player;

    Vector3 pivot;



    // Start is called before the first frame update
    void Start()
    {
        pivot = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    // Update is called once per frame
    void Update()
    {

        transform.position = pivot;
        if (Camera.main)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
        
        transform.Translate(transform.forward * -0.5f, Space.World);

        image.transform.localScale = new Vector3(GetComponentInParent<Resources>().GetHealth(), 1f, 0.5f);
    }
}
