using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaldronManager : MonoBehaviour
{

    [SerializeField]Image itemImage;
    [SerializeField] Image preparationImage;
    public CraftedIngredient selectedItem=null;
    public List<CauldronSlot> slotList;
    public Button button;


    [SerializeField]Canvas canvas;
    public static CaldronManager instance;
    
    private void OnEnable()
    {
        Ingredient.OnClicked += ChangeItem;
    }
    private void OnDisable()
    {
        Ingredient.OnClicked -= ChangeItem;
    }
    public void SetPreparation(Preparation item)
    {
        selectedItem.preparation=item;
        preparationImage.sprite = item.image;
    }
    public void ChangeItem(CraftedIngredient item)
    {
        selectedItem=item;
        preparationImage.sprite=item.preparation.image;
        itemImage.sprite = item.itemData.image;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void CheckSlots()
    {
        foreach (var slot in slotList)
        {
           if(slot.item.itemData.id == 0)
            {
                button.interactable = false;
                return;
            }
        }
        button.interactable = true;
    }



}
    





