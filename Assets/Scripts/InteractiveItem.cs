using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveItem : MonoBehaviour
{
    public Item itemScript;
    public int id;
    public int amountObject;



    private void Start()
    {
        id = itemScript.id;
    }

    public InteractiveItem(Item item, int amount)
    {
        item  = itemScript;
        amount = amountObject;
    }



}

