using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static ChestManager instance;

    public GameObject bigContainerUI;
    public GameObject smallContainerUI;

    public GameObject openChestCurrent;



    private void Awake()
    {
        instance = this;

    }

}
