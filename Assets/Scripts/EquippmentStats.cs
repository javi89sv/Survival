using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquippmentStats : MonoBehaviour
{
    Animator anim;
    public EquipmentObject itemObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        //IGNORA LOS CLICKS EN LA UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Hit");
            }
        }
    }

}
