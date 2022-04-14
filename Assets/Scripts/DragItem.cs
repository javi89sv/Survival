using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Slot dragItem;
    public static GameObject itemDragging;
    public Image dragImage;
    public CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform startParent;

    private GameObject itemBeingDragged;


    private void Start()
    {

        dragItem = GetComponent<Slot>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragItem.item != null)
        {
            itemBeingDragged = GetComponentInChildren<InteractiveItem>().gameObject;

            dragImage.sprite = dragItem.item.icon;
            dragImage.transform.position = Input.mousePosition;
            dragImage.enabled = true;

            startPosition = itemBeingDragged.transform.position;
            startParent = itemBeingDragged.transform.parent;

            itemBeingDragged.transform.SetParent(transform.root);

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

        itemBeingDragged = null;

    }

}
