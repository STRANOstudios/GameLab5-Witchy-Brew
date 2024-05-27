using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegendPanelScript : MonoBehaviour
{
    public PotionGenerator potionGenerator;
    public TMP_Text text;
    public GameObject legendPanel;

    private void Start()
    {
        text.text = 
            "<size=32>Legend:</size>\n" +
            "<color=#" + ColorUtility.ToHtmlStringRGB(potionGenerator.correctColor) + ">\u25a0</color>: Correct position, correct preparation\n" +
            "<color=#" + ColorUtility.ToHtmlStringRGB(potionGenerator.wrongPositionColor) + ">\u25a0</color>: Wrong position, correct preparation\n" +
            "<color=#" + ColorUtility.ToHtmlStringRGB(potionGenerator.wrongPreparationColor) + ">\u25a0</color>: Correct position, wrong preparation\n" +
            "<color=#" + ColorUtility.ToHtmlStringRGB(potionGenerator.wrongPositionAndPreparationColor) + ">\u25a0</color>: Wrong position, wrong preparation\n" +
            "<color=#" + ColorUtility.ToHtmlStringRGB(potionGenerator.ingredientNotPresentColor) + ">\u25a0</color>: Wrong ingredient";
        
        
    }

    public void toggleVisibility()
    {
        legendPanel.SetActive(!legendPanel.activeSelf);
    }
    
    
}
