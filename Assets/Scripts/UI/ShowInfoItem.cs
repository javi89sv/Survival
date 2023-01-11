using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowInfoItem : MonoBehaviour
{

    public TextMeshProUGUI nameInfo;
    public TextMeshProUGUI descriptionInfo;
    public Image imageInfo;

    public void ShowInfo(InventorySlot slot)
    {
        //nameInfo.text = slot.item.name.ToString();
        descriptionInfo.text = slot.item.name.ToString();
        imageInfo.sprite = slot.item.icon;

        gameObject.SetActive(true);
    }

    public void ResetInfo()
    {
        //nameInfo.text = "";
        descriptionInfo.text = "";
        imageInfo.sprite = null;
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }

    //public void DropItem()
    //{
    //    if(InventoryManager.instance.slotSelected != null)
    //    {
    //        InventoryManager.instance.slotSelected.DropItem();
    //        HideInfo();
    //    }
    //}

    //public void UseItem()
    //{
    //    if (InventoryManager.instance.slotSelected != null)
    //    {
    //        InventoryManager.instance.slotSelected.DropItem();
    //        UseItem();
    //    }
    //}

}
