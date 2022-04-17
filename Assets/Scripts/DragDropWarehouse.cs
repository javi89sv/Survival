using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropWarehouse : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
            itemDragging = GetComponentInChildren<InteractiveItem>().gameObject;

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

}