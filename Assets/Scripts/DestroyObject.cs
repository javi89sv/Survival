using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timeToDestroy;

    private void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

}
