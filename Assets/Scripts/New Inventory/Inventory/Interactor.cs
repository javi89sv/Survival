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
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionPointRadius, interactionLayer))
        {
            var interactable = hit.collider.GetComponent<IInterectable>();

            if (interactable != null)
            {
                InfoUI.Instance.SetTooltipItem(hit.collider.name + "\n" + interactable.TextInfo());
            }

            if (Input.GetKey(KeyCode.E))
            {
                if (interactable != null)
                {
                    
                    interactable.Interact(this);
                    InfoUI.Instance.HideText();


                }
            }

        }
        else
        {
            InfoUI.Instance.ClearText();
            InfoUI.Instance.ShowText();
        }

    }

}
