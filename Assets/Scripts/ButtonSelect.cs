using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour
{

    public static ButtonSelect instance;

    public GameObject go;
    public Slot slot;
    // Start is called before the first frame update

    public void UseItem()
    {
        go.GetComponent<Slot>().UseItem();
    }

    private void Start()
    {
        instance = this;
    }
}
