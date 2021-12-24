using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnItems : MonoBehaviour
{
   public GameObject[] spawnItems;
   private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
        spawnPosition = new Vector3(Random.Range(0f, 5f), Random.Range(0f, 5f), Random.Range(0f, 5f));

    }

    private void SpawnItem()
    {

        for (int i = 0; i < spawnItems.Length; i++)
        {
            Instantiate(spawnItems[i], spawnPosition, Quaternion.identity);
        }
    }


}
