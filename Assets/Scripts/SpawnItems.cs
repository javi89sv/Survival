using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnItems : MonoBehaviour
{
   public GameObject[] spawnItems;
   public Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
       // SpawnItem();

    }

    private void SpawnItem()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }
        for (int i = 0; i < spawnItems.Length; i++)
        {
            PhotonNetwork.Instantiate("Equippement/" + spawnItems[i].name, spawnPosition, Quaternion.identity);
        }
    }


}
