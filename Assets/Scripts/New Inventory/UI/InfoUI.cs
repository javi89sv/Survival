using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InfoUI : MonoBehaviour
{
    public static InfoUI Instance;

    public TextMeshProUGUI tooltipText;
    public Image tooltipImage;
    public bool isHovering;

    private void Awake()
    {
        Instance = this;
    }
    public void SetTooltipItem(string itemName)
    {
        isHovering = true;
        tooltipText.text = itemName;
    }   
    public void SetTooltipInteractable(string itemName)
    {
        isHovering = true;
        tooltipText.text = itemName + "\n Pulsa E para interactuar";
    }
    public void HideText()
    {
        isHovering = false;
        tooltipText.text = string.Empty;
    }

    private void Update()
    {
        if (!isHovering)
        {
            tooltipText.text = string.Empty;
        }
    }
}
