using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour
{
    public static LootSystem instance;

    public int random;

    public void Start()
    {
        instance = this;
    }

    public void Loot(LootTable lootTable, Vector3 transformSpawn)
    {
        for (int i = 0; i < lootTable.drop.Length; i++)
        {
            random = Random.Range(0, 100);
            print(random);
            if (random <= lootTable.drop[i].chanceLoot)
            {
                GameObject lootPrefab = Instantiate(lootTable.drop[i].prefabLoot, transformSpawn + lootTable.drop[i].SpawnOffset, Quaternion.identity);
                lootPrefab.name = lootPrefab.name.Replace("(Clone)", "");
                lootPrefab.GetComponent<ItemPickUp>().amount = (int)Random.Range(lootTable.drop[i].minAmount, lootTable.drop[i].maxAmount);
                print("Drop!!" + lootTable.drop[i].prefabLoot.name);
            }
        }
    }
}
