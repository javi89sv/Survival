using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateItems : MonoBehaviour
{
    public ItemObject[] items;
    public Button button;


    public void Generate()
    {
        foreach (var item in items)
        {
           GameObject go = Instantiate(item.prefab);
           go.name = go.name.Replace("(Clone)", "");
        }
    }

}
