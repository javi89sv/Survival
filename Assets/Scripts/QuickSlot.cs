using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    public static QuickSlot instance;

    private Transform slots = null;
    private int slotID;

    private void Awake()
    {
        slots = transform.Find("SlotHolder");
    }

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (slotID >= slots.childCount - 1)
            {
                slotID = 0;

            }
            else
            {
                slotID++;
                Selection();
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (slotID <= 0)
            {
                slotID = slots.childCount - 1;

            }
            else
            {
                slotID--;
                Selection();
            }
        }
    }

    private void Selection()
    {
        throw new NotImplementedException();
    }
}
