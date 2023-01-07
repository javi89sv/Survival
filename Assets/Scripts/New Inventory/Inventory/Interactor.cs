using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionRange = 1f;
    public static bool isInteraction;

    public float pickupTime = 5.0f; // Umbral de tiempo que se debe dejar pulsada la tecla para recoger el item
    private float timeHeld = 0.0f; // Tiempo que lleva pulsada la tecla

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionRange))
        {
            var interactable = hit.collider.GetComponent<IInterectable>();
            var hitable = hit.collider.GetComponent<IHitable>();
            var removable = hit.collider.GetComponent<IRemovable>();

            if (interactable != null)
            {
                InfoUI.Instance.SetTooltipItem(hit.collider.name + "\n" + interactable.TextInfo());
            }


            if(hitable != null && hitable.CurrentHealth() >= 0)
            {             
                InfoUI.Instance.ShowBarHealth();
                InfoUI.Instance.UpdateBarHealth(hitable.CurrentHealth(), hitable.MaxHealth());
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

            if(hit.collider.GetComponent<RemoveItemsGround>())
            {
                if (Input.GetKey(KeyCode.T)) // Comprueba si se está pulsando la tecla E
                {
                    timeHeld += Time.deltaTime; // Aumenta el tiempo pulsada en cada frame
                    if (timeHeld >= pickupTime) // Si el tiempo pulsada supera el umbral, recoge el item
                    {
                        hit.collider.GetComponent<RemoveItemsGround>().Interact(this);
                    }
                }
                else // Si no se está pulsando la tecla, resetea el contador
                {
                    timeHeld = 0.0f;
                }
            }


        }
        else
        {
            InfoUI.Instance.ClearText();
            InfoUI.Instance.ShowText();
            InfoUI.Instance.HideBarHealth();
        }


    }

    private void RemovePlaceItem(RemoveItemsGround item)
    {
        if (Input.GetKey(KeyCode.T)) // Comprueba si se está pulsando la tecla E
        {
            timeHeld += Time.deltaTime; // Aumenta el tiempo pulsada en cada frame
            if (timeHeld >= pickupTime) // Si el tiempo pulsada supera el umbral, recoge el item
            {

            }
        }
        else // Si no se está pulsando la tecla, resetea el contador
        {
            timeHeld = 0.0f;
        }
    }

}
