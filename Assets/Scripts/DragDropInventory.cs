using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
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
            return;
        }

        if (!this.GetComponent<Slot>().empty)
        {
            return;
        }

        if (this.GetComponent<Slot>().id == eventData.pointerDrag.GetComponent<Slot>().id && eventData.pointerDrag.GetComponent<Slot>().item.isStackable)
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
            if (eventData.pointerDrag.GetComponent<Slot>().maxStackSize)
            {
                eventData.pointerDrag.GetComponent<Slot>().maxStackSize = false;
            }
            eventData.pointerDrag.GetComponent<Slot>().CleanSlot();

        }

    }

}
