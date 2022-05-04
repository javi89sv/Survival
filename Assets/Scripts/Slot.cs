using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public enum TypeSlot
    {
        slotInventory,
        slotHotbar,
        slotContainer
    }

    public TypeSlot typeSlot;

    public Item item;
    public GameObject prefab;
    public Image iconSlot;
    public TextMeshProUGUI textAmount;
    public Image conditionBar;

    public int id;
    public bool maxStackSize;
    public bool empty;
    public int amount;
    public int condition;

    private GameObject player;

    [Header("--Panel Info--")]
    public ShowInfoItem infoUI;

    private void Awake()
    {
        UpdateSlot();
    }

    void Start()
    {

        infoUI = FindObjectOfType<ShowInfoItem>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {

        if (item)
        {
            if (amount >= item.maxStack)
            {
                maxStackSize = true;
            }
        }
        if(amount < 1)
        {
            CleanSlot();
        }

    }

    public void UpdateSlot()
    {
        UpdateEmpty();
        UpdateAmount();
        UpdateIcon();
        UpdateConditionBar();
        UpdatePrefab();
        UpdateID();
        
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

    void UpdatePrefab()
    {
        if (item == null)
        {
            prefab = null;
        }
        else
        {
            prefab = item.prefab;

        }
    }

    void UpdateIcon()
    {
        if (item == null)
        {
            iconSlot.enabled = false;
        }
        else
        {
            iconSlot.enabled = true;
            iconSlot.sprite = item.icon;
        }
    }

    void UpdateID()
    {
        if (item == null)
        {
            id = 0;
        }
        else
        {
            id = item.id;
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

    void UpdateEmpty()
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

    public void DropItem()
    {
        

        if (GameManager.instance.weapon != null)
        {
            GameManager.instance.weapon.gameObject.SetActive(false);
        }

        GameObject goDropped = Instantiate(prefab);

        Debug.Log("Drop item!!");
        goDropped.name = goDropped.name.Replace("(Clone)", "");
        goDropped.transform.SetParent(null);
        goDropped.GetComponent<Renderer>().enabled = true;
        goDropped.GetComponent<Collider>().enabled = true;
        goDropped.GetComponent<Rigidbody>().isKinematic = false;
        goDropped.GetComponent<InteractiveItem>().quantity = amount;
        goDropped.transform.position = new Vector3(player.transform.position.x + 1f, player.transform.position.y + 1f, player.transform.position.z + 1f);
        goDropped.GetComponent<Rigidbody>().AddForce(player.transform.forward * 100);

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

                consumable.UseConsumable();

                amount -= 1;

                UpdateSlot();
                Debug.Log("Usamos item ");
            }

            if (item.type == Item.Type.equippable)
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
            infoUI.ShowInfo(this);
            InventoryManager.instance.slotSelected = this;
        }

        else if (pointerEventData.button == PointerEventData.InputButton.Right)
            UseItem();

    }

    public void TakeDamage(int damage)
    {
        condition -= damage;
    }


}

