using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScipt : MonoBehaviour
{

    [SerializeField]
    private GameObject slotPrefab;
    
    public void AddSlots(int slotCount)
    {
        
        for (int i = 0; i < slotCount; i++)
        {
            GameObject.Instantiate(slotPrefab, transform);
        }
    }

    private void Awake()
    {
        AddSlots(15);
    }

}
