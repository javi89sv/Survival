using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{

    public static ButtonSelect instance;

    public GameObject go;


    private void Start()
    {
        instance = this;
    }

    public void UseItem()
    {
        go.GetComponent<Slot>().UseItem();
    }

}
