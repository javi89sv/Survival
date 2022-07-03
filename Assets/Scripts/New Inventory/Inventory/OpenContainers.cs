using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenContainers : InventoryHolder, IInterectable
{
    public string textInfo;
    public UnityAction<IInterectable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public string TextInfo()
    {
        throw new System.NotImplementedException();
    }


}
