using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropWarehouse : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Slot dragItem;
    public Image dragImage;
    public CanvasGroup canvasGroup;


    private void Start()
    {

        dragItem = GetComponent<Slot>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragItem.item != null)
        {

            dragImage.sprite = dragItem.item.icon;
            dragImage.transform.position = Input.mousePosition;
            dragImage.enabled = true;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragImage.transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        GetComponent<Image>().raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        dragImage.enabled = false;

    }

    public void OnDrop(PointerEventData eventData)
    {

        if (!this.GetComponent<Slot>())
        {
            eventData.pointerDrag.GetComponent<Slot>().DropItem();
        }

        else
        {
            if (!this.GetComponent<Slot>().empty && this.GetComponent<Slot>().id == eventData.pointerDrag.GetComponent<Slot>().id && this.GetComponent<Slot>().maxStackSize != true)
            {
                this.GetComponent<Slot>().amount += eventData.pointerDrag.GetComponent<Slot>().amount;
                this.GetComponent<Slot>().empty = false;
                this.GetComponent<Slot>().UpdateSlot();
                eventData.pointerDrag.GetComponent<Slot>().CleanSlot();

            }

            if (this.GetComponent<Slot>().empty && !this.GetComponent<Slot>().maxStackSize)
            {
                if (GameManager.instance.weapon != null)
                {
                    GameManager.instance.weapon.gameObject.SetActive(false);
                }

                Debug.Log("On Drop");
                this.GetComponent<Slot>().item = eventData.pointerDrag.GetComponent<Slot>().item;
                this.GetComponent<Slot>().amount = eventData.pointerDrag.GetComponent<Slot>().amount;
                this.GetComponent<Slot>().empty = false;
                this.GetComponent<Slot>().UpdateSlot();
                LootWarehouse lootWarehouse = ChestManager.instance.openChestCurrent.GetComponent<LootWarehouse>();

                for (int i = 0; i < lootWarehouse.loot.Count; i++)
                {
                    if (lootWarehouse.loot[i] == null)
                    {

                        lootWarehouse.loot[i] = eventData.pointerDrag.GetComponent<Slot>().prefab;
                        break;
                    }
                }



                if (eventData.pointerDrag.GetComponent<Slot>().maxStackSize)
                {
                    eventData.pointerDrag.GetComponent<Slot>().maxStackSize = false;
                }
                eventData.pointerDrag.GetComponent<Slot>().CleanSlot();

            }

        }


    }

}