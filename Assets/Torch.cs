using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject light;
    public ParticleSystem fire;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (light.activeInHierarchy)
            {
                light.SetActive(false);
            }
            else
            {
                light.SetActive(true);
            }
            
        }
    }
}
