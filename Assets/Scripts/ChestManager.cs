using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static ChestManager instance;

    public GameObject bigContainerUI;
    public GameObject smallContainerUI;

    public GameObject openChestCurrent;

    public LootWarehouse[] lootContainer;

    private void Awake()
    {
        instance = this;

        lootContainer = FindObjectsOfType<LootWarehouse>();

    }

}
