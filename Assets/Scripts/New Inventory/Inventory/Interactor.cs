using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionPointRadius = 1f;
    public static bool isInteraction;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionPointRadius))
        {
            var interactable = hit.collider.GetComponent<IInterectable>();
            var hitable = hit.collider.GetComponent<IHitable>();

            if (interactable != null)
            {
                InfoUI.Instance.SetTooltipItem(hit.collider.name + "\n" + interactable.TextInfo());
            }
            else
            {
                InfoUI.Instance.ClearText();
                InfoUI.Instance.ShowText();
            }

            if(hitable != null)
            {             
                InfoUI.Instance.ShowBarHealth();
                InfoUI.Instance.UpdateBarHealth(hitable.Health());
            }
            else
            {

                InfoUI.Instance.HideBarHealth();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable != null)
                {
                    
                    interactable.Interact(this);
                    InfoUI.Instance.HideText();


                }
            }

        }


    }

}
