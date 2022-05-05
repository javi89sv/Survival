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
        for (int i = 0; i < lootTable.chance.Length; i++)
        {
            random = Random.Range(0, 100);
            print(random);
            if (random <= lootTable.chance[i].chanceLoot)
            {
                GameObject lootPrefab = Instantiate(lootTable.chance[i].prefabLoot, transformSpawn + lootTable.chance[i].SpawnOffset, Quaternion.identity);
                lootPrefab.GetComponent<InteractiveItem>().amountObject = (int)Random.Range(lootTable.chance[i].minAmount, lootTable.chance[i].maxAmount);
                print("Drop!!" + lootTable.chance[i].prefabLoot.name);
            }
        }
    }
}
