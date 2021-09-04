using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {


        Debug.Log("On Drop");

        if (!this.GetComponent<Slot>())
        {
            Debug.Log("Drop item!!");
            eventData.pointerDrag.GetComponent<Slot>().DropItem();
        }
        else
        {
            if (!this.GetComponent<Slot>().empty && this.GetComponent<Slot>().id == eventData.pointerDrag.GetComponent<Slot>().id && this.GetComponent<Slot>().maxStackSize != true)
            {
                this.GetComponent<Slot>().amount += eventData.pointerDrag.GetComponent<Slot>().amount;
                this.GetComponent<Slot>().empty = false;             
                eventData.pointerDrag.GetComponent<Slot>().CleanSlot();
                this.GetComponent<Slot>().UpdateSlot();

                
            }

            if (this.GetComponent<Slot>().empty && !this.GetComponent<Slot>().maxStackSize)
            {
                if (GameManager.instance.weapon != null)
                {
                    GameManager.instance.weapon.gameObject.SetActive(false);
                }
                Debug.Log("On Drop");
                this.GetComponent<Slot>().item = eventData.pointerDrag.GetComponent<Slot>().item;
                this.GetComponent<Slot>().prefab = eventData.pointerDrag.GetComponent<Slot>().prefab;
                this.GetComponent<Slot>().id = eventData.pointerDrag.GetComponent<Slot>().id;
                this.GetComponent<Slot>().amount = eventData.pointerDrag.GetComponent<Slot>().amount;
                this.GetComponent<Slot>().empty = false;
                if (eventData.pointerDrag.GetComponent<Slot>().maxStackSize)
                {
                    eventData.pointerDrag.GetComponent<Slot>().maxStackSize = false;
                }
                eventData.pointerDrag.GetComponent<Slot>().CleanSlot();
                this.GetComponent<Slot>().UpdateSlot();

            }

        }


    }
}