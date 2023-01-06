using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject _light;
    public ParticleSystem fire;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_light.activeInHierarchy)
            {
                _light.SetActive(false);
            }
            else
            {
                _light.SetActive(true);
            }
            
        }
    }
}
