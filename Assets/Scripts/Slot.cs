using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler
{

    public Item item;
    public InteractiveItem interactiveItem;
    public int id;
    public bool maxStackSize;
    public bool empty;

    private int _amount;
    public int amount
    {
        get { return _amount; }
        set
        {

            if (item == null) _amount = 0; // Can't have an amount of no item.
            else if (value > item.maxStack) _amount = item.maxStack; // Ensure we don't end up with more items than can stack.
            else if (value < 1) _amount = 0; // Can't have a minus amount of something.
            else _amount = value;


        }
    }

    private int _condition;
    public int condition
    {
        get { return _condition; }
        set
        {

            if (item == null) _condition = 0;
            else if (value > item.maxDegradable) _condition = item.maxDegradable;
            else if (value < 1) _condition = 0;
            else _condition = value;


        }
    }

    public GameObject player;
    public GameObject prefab;
    private TextMeshProUGUI textAmount;
    public TextMeshProUGUI textInfo;
    public GameObject panelInfo;
    public Image imageInfo;
    public Image iconSlot;
    private Image conditionBar;

    public GameObject[] slotsBar;


    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        iconSlot = transform.GetChild(0).GetComponent<Image>();
        textAmount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        conditionBar = transform.GetChild(2).GetComponent<Image>();

        CheckEmpty();

    }


    private void CheckEmpty()
    {
        if (item == null)
        {
            empty = true;
        }
        else
        {
            empty = false;
        }
    }

    private void Update()
    {

        if (amount == 0)
        {
            CleanSlot();
        }

        if (amount >= 1000)
        {
            maxStackSize = true;

        }

    }

    public void UpdateSlot()
    {
        UpdateAmount();
        UpdateIcon();
        UpdateConditionBar();
    }

    void UpdateAmount()
    {
        if (item == null || this.empty || amount < 2)
        {
            textAmount.enabled = false;
        }
        else
        {
            textAmount.enabled = true;
            textAmount.text = this.amount.ToString();

        }
    }

    void UpdateIcon()
    {
        if (item == null || this.empty)
        {
            iconSlot.enabled = false;
        }
        else
        {
            iconSlot.enabled = true;
            iconSlot.sprite = item.icon;
        }
    }

    void UpdateConditionBar()
    {
        if (item == null || !item.isDegradable)
        {
            conditionBar.enabled = false;
        }
        else
        {

            conditionBar.enabled = true;

            float conditionPercent = (float)condition / (float)item.maxDegradable;

            float barHeight = conditionBar.GetComponent<RectTransform>().rect.height * conditionPercent;

            conditionBar.rectTransform.sizeDelta = new Vector2(conditionBar.rectTransform.sizeDelta.x, barHeight);

        }
    }

    public void DropItem()
    {

        if (GameManager.instance.weapon != null)
        {
            GameManager.instance.weapon.gameObject.SetActive(false);
        }

        GameObject goDropped = GetComponentInChildren<InteractiveItem>().gameObject;

        Debug.Log("Drop item!!");
        goDropped.transform.SetParent(null);
        goDropped.GetComponent<Renderer>().enabled = true;
        goDropped.GetComponent<Collider>().enabled = true;
        goDropped.GetComponent<Rigidbody>().isKinematic = false;
        goDropped.GetComponent<InteractiveItem>().quantity = amount;
        goDropped.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
        goDropped.GetComponent<Rigidbody>().AddForce(player.transform.forward * 200);

        CleanSlot();

    }

    public void CleanSlot()
    {
        prefab = null;
        item = null;
        id = 0;
        amount = 0;
        empty = true;

        UpdateSlot();
    }

    public void UseItem()
    {

        if (!empty)
        {

            if (item.type == Item.Type.consumable)
            {
                Consumable consumable = prefab.GetComponent<Consumable>();

                player.GetComponent<PlayerManager>().currentHealth += consumable.health;
                player.GetComponent<PlayerManager>().currentFood += consumable.food;
                player.GetComponent<PlayerManager>().currentDrink += consumable.drink;

                amount -= 1;

                UpdateSlot();
                Debug.Log("Usamos item ");
            }
            else if(item.type == Item.Type.equippable)
            {
                int listWeapons = player.GetComponent<Equippement>().eqquipmentList.Length;

                for (int i = 0; i < listWeapons; i++)
                {
                    if (player.GetComponent<Equippement>().eqquipmentList[i].name == item.name)
                    {
                        player.GetComponent<Equippement>().Equip(i);
                    }
                }

                UpdateSlot();
                Debug.Log("Equipamos item ");
            }
            else
            {
                Debug.Log("Cant use this");
            }

        }

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {

        if (pointerEventData.button == PointerEventData.InputButton.Left && item)
        {
            panelInfo.SetActive(true);
            ShowInfoItem();
        }

        else if (pointerEventData.button == PointerEventData.InputButton.Right)
            UseItem();

    }

    public void TakeDamage(int damage)
    {
        condition -= damage;
    }

    public void ShowInfoItem()
    {
        imageInfo.sprite = item.icon;
        textInfo.text = item.name.ToString() + "\r\n" + item.description.ToString();
        ButtonSelect.instance.go = this.gameObject;
    }



    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (item)
    //    {
    //        panelInfo.SetActive(true);
    //        ShowInfoItem();
    //    }
    //    else
    //    {
    //        panelInfo.SetActive(false);
    //    }

    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    panelInfo.SetActive(false);
    //}
}

