using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public LayerMask interactionLayer;
    public float interactionPointRadius = 1f;
    public static bool isInteraction;

    private void Update()
    {
        var collider = Physics.OverlapSphere(interactionPoint.position, interactionPointRadius, interactionLayer);
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionPointRadius))
        {
            if (hit.collider.GetComponent<InventoryHolder>())
            {
                var interactable = hit.collider.GetComponent<IInterectable>();
                var invHolder = hit.collider.GetComponent<InventoryHolder>();

                if (Input.GetKey(KeyCode.E))
                {
                    if (interactable != null)
                    {
                        StartInteraction(interactable);
                        InventoryUIController.instance.namePanelText.text = invHolder.nameContainer.ToUpper();
                        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested?.Invoke(PlayerInventoryHolder.instance.SecondaryInventorySystem);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;

                    }
                }
            }
        }

    }

    private void StartInteraction(IInterectable interactable)
    {
        interactable.Interact(this, out bool interactSucessful);
        isInteraction = true;
    }

    void EndInteraction()
    {
        isInteraction = false;
    }
}
