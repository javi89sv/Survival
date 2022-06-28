using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueCraft : MonoBehaviour
{
    [SerializeField] ItemObject[] list;
    public Queue<ItemObject> items = new Queue<ItemObject>();

    private void Start()
    {
        StartQueue();
        
    }

    public void StartQueue()
    {
        foreach(ItemObject item in list)
        {
            items.Enqueue(item);
        }

        foreach(ItemObject item in items)
        {
            Debug.Log(item);
        }

    }



}
