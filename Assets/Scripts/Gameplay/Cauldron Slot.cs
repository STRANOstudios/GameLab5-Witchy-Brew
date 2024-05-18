using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronSlot : MonoBehaviour
{
    [SerializeField]CraftedIngredient NullItem;
    public CraftedIngredient item;
    private string id;
    public Image image;
    private void Start()
    {
        item= NullItem;
        image = GetComponent<Image>();
        image.sprite = NullItem.itemData.image;
        id=name;
    }

    public void SetItem()
    {
        CaldronManager instance=CaldronManager.instance;

        CraftedIngredient temp = instance.selectedItem;
        instance.ChangeSprite(this.item);
        item = temp;
        image.sprite = temp.itemData.image;
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
            this.item =NullItem;
            this.image.sprite = NullItem.itemData.image;
        }
    }

}
