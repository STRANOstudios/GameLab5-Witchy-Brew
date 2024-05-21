using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronSlot : MonoBehaviour
{
    [SerializeField]CraftedIngredient NullItem;
    public CraftedIngredient item;
    private string id;
    [SerializeField]Image itemImage;
    [SerializeField] Image preparationImage;

    private void Start()
    {
        item= NullItem;
        //itemImage = GetComponent<Image>();
        itemImage.sprite = NullItem.itemData.image;
        //preparationImage = GetComponentInChildren<Image>();
        preparationImage.sprite = NullItem.preparation.image;
        id =name;
    }



    public void SetItem()
    {
            CaldronManager instance = CaldronManager.instance;
            CraftedIngredient temp = instance.selectedItem;
            instance.ChangeItem(this.item);
            item = temp;
            itemImage.sprite = temp.itemData.image;
            preparationImage.sprite = temp.preparation.image;
            instance.CheckSlots();
    }
    private void OnEnable()
    {
        Ingredient.OnClicked += RemoveItem;
    }
    private void OnDisable()
    {
        Ingredient.OnClicked -= RemoveItem;
    }
    void RemoveItem(CraftedIngredient item)
    {
        if (item.itemData.id == this.item.itemData.id)
        {
            this.item = NullItem;
            this.preparationImage.sprite=NullItem.preparation.image;
            this.itemImage.sprite = NullItem.itemData.image;
            CaldronManager.instance.CheckSlots();
        }
    }

}
