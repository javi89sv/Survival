using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveItem : MonoBehaviour
{
    public Item item;
    public int id;
    public int quantity;



    private void Start()
    {
        id = item.id;
    }



}

