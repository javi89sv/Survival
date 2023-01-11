using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudUI : MonoBehaviour
{
    public static HudUI instance;

    public Image barHealthPlayer;
    public Image barHungryPlayer;
    public Image barThirstPlayer;

    public TextMeshProUGUI textGettedItem;

    public GameObject panelInfo;
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI descriptionItem;
    public Image iconItem;

    [SerializeField] Button exit;

    PlayerManager player;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        UpdateBarsStats();
    }

    public void UpdateBarsStats()
    {
        barHealthPlayer.fillAmount = player.currentHealth / player.maxHealth;
        barThirstPlayer.fillAmount = player.currentThirst / player.maxThirst;
        barHungryPlayer.fillAmount = player.currentHungry / player.maxHungry;
    }

    public void UpdateText(string name, int amount)
    {
        textGettedItem.CrossFadeAlpha(1f, 0f, false);
        textGettedItem.text = "+ " + amount + " " + name;
        textGettedItem.CrossFadeAlpha(0.0f, 5f, false);
    }

    public void UpdateInfo(string name, string descr, Image icon)
    {
        nameItem.text = name;
        descriptionItem.text = descr;
        iconItem.sprite = icon.sprite;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
