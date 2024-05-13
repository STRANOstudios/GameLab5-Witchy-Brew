using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CaldronManager : MonoBehaviour
{
    [SerializeField]Image image;
    ItemData selectedItem;
    [SerializeField] static int cauldronSize;
    ItemData[] item=new ItemData[cauldronSize];
    [SerializeField]Button[] button=new Button[cauldronSize];

    private void OnEnable()
    {
        Ingredient.OnClicked += ChangeSprite;
    }
    private void OnDisable()
    {
        Ingredient.OnClicked -= ChangeSprite;
    }
    private void ChangeSprite(ItemData item)
    {
        selectedItem=item;
        image.sprite = item.image;
    }
    public void AddItem()
    {
        for (int i = 0; i < button.Length; i++)
        {
            if (button[i] ==selectedItem)
            {

            }
        }
    }

    }
    





