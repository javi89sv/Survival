using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IInterectable
{
    public UnityAction<IInterectable> OnInteractionComplete { get; set; }
    public void Interact(Interactor interactor, out bool interactSucessful);
    public void EndInteraction();


}
