using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudUI : MonoBehaviour
{

    public Image barHealthPlayer;
    public Image barHungryPlayer;
    public Image barThirstPlayer;
    public TextMeshProUGUI textGettedItem;

    PlayerManager player;

    private void Awake()
    {
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
}
