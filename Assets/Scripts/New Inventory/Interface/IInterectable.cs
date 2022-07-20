using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IInterectable
{
    
    public void Interact(Interactor interactor);

    public string TextInfo();



}
