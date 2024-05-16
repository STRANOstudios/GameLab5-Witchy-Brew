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
    public void AddItem(int i)
    {
        button[i].image.sprite = selectedItem.image;
    }

}
    





