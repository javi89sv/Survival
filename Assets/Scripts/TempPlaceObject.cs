using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlaceObject : MonoBehaviour
{

    public List<Collider> colliders = new List<Collider>();
    public Material red;
    public Material green;
    public bool isBuildable;

    private void Update()
    {
        ChangeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 0)
        {
            colliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 0)
        {
            colliders.Remove(other);
        }
    }

    public void ChangeColor()
    {
        if(colliders.Count == 0)
        {
            isBuildable = true;
        }
        else
        {
            isBuildable = false;
        }

        if (isBuildable)
        {
            GetComponent<MeshRenderer>().material = green;
        }
        else
        {
            GetComponent<MeshRenderer>().material = red;
        }

    }

}
