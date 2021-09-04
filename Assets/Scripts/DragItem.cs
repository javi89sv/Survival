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
    private Transform slot;


    private void Start()
    {

        dragItem = GetComponent<Slot>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragItem.item != null)
        {
            itemDragging = gameObject;
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
        itemDragging = null;
        GetComponent<Image>().raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        dragImage.enabled = false;

    }

}
