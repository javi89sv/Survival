using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Slot dragItem;
    public static GameObject itemDragging;
    public Image dragImage;
    public CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform startParent;



    private void Start()
    {

        dragItem = GetComponent<Slot>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragItem.item != null)
        {
            itemDragging = GetComponent<Slot>().prefab;

            dragImage.sprite = dragItem.item.icon;
            dragImage.transform.position = Input.mousePosition;
            dragImage.enabled = true;

            startPosition = itemDragging.transform.position;
            startParent = itemDragging.transform.parent;

            //itemDragging.transform.SetParent(transform.root);

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
        

        if (itemDragging.transform.parent == startParent || itemDragging.transform.parent == transform.root)
        {
            //itemBeingDragged.transform.position = startPosition;
            itemDragging.transform.SetParent(startParent);

        }

      

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
