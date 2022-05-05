using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : InventoryManager
{

    [SerializeField]
    private GameObject slotPrefab;

    public List<Slot> slots;
    

    private void Awake()
    {
       // AddSlots(15);
    }

}
