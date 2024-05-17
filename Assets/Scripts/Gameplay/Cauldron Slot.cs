using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronSlot : MonoBehaviour
{
    [SerializeField]ItemData NullItem;
    public ItemData item;
    private string id;
    public Image image;
    private void Start()
    {
        item = NullItem;
        image = GetComponent<Image>();
        image.sprite = NullItem.image;
        id=name;
    }

    public void SetItem()
    {
        CaldronManager instance=CaldronManager.instance;

        ItemData temp = instance.selectedItem;
        instance.ChangeSprite(this.item);
        item = temp;
        image.sprite = temp.image;
    }
    private void OnEnable()
    {
        Ingredient.OnClicked += RemoveItem;
    }
    private void OnDisable()
    {
        Ingredient.OnClicked -= RemoveItem;
    }
    void RemoveItem(ItemData item)
    {
        if (item.id == this.item.id)
        {
            this.item=NullItem;
            this.image.sprite = NullItem.image;
        }
    }

}
