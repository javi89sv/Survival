using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InfoUI : MonoBehaviour
{
    public static InfoUI Instance;

    public TextMeshProUGUI tooltipText;
    public Image pointCenter;
    public bool isHovering;
    public GameObject healthBar;
    public Image barImage;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowBarHealth()
    {
        healthBar.SetActive(true);
    }

    public void HideBarHealth()
    {
        healthBar.SetActive(false);
    }

    public void SetTooltipItem(string itemName)
    {
        isHovering = true;
        tooltipText.text = itemName;
        pointCenter.enabled = false;
    }

    public void ClearText()
    {
        isHovering = false;
        tooltipText.text = string.Empty;
        pointCenter.enabled = true;
    }

    public void ShowText()
    {
        tooltipText.alpha = 1;
    }

    public void HideText()
    {
        tooltipText.alpha = 0;
    }

    private void Update()
    {
        if (isHovering == false)
        {
            tooltipText.text = string.Empty;
            pointCenter.enabled = true;
        }
    }

    public void UpdateBarHealth(int value)
    {
        if (healthBar.activeInHierarchy)
        {
            barImage.fillAmount = (float)value / 100f;
        }
        
    }
}
