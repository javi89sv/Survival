using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxLoot : InventoryHolder, IInterectable
{

    public string textInfo;

    public UnityAction<IInterectable> OnInteractionComplete { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public static UnityAction<InventorySystem> OnBoxInventoryDisplayRequested;

    public void Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public string TextInfo()
    {
        return textInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
