using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDropInventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

}
