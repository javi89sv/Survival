using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateItems : MonoBehaviour
{
    public ItemObject[] items;
    public Button button;
    // Start is called before the first frame update

    public void Generate()
    {
        foreach (var item in items)
        {
            Instantiate(item.prefab);
        }
    }

}
