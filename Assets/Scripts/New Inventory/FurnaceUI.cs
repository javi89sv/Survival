using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceUI : MonoBehaviour
{
    public Button buttonOn;
    public Button buttonOff;

    public bool isRun;

    public Furnace furnaceCurrent;

    public void Update()
    {
        if (furnaceCurrent.GetComponent<BurningSystem>().isRun)
        {
            buttonOn.gameObject.SetActive(false);
            buttonOff.gameObject.SetActive(true);
        }
        if (!furnaceCurrent.GetComponent<BurningSystem>().isRun)
        {
            buttonOn.gameObject.SetActive(true);
            buttonOff.gameObject.SetActive(false);
        }
    }

    public void RunFurnace()
    {
        if (furnaceCurrent.GetComponent<BurningSystem>().Run())
        {
            isRun = true;
        }
        else
        {
            Debug.LogWarning("Need Wood");
        }

    }

    public void StopFurnace()
    {

        furnaceCurrent.GetComponent<BurningSystem>().Stop();

    }

}
