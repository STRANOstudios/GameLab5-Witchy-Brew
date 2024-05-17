using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronSlot : MonoBehaviour
{
    public ItemData item;
    private string id;
    [SerializeField]public Image image;
    public Sprite NullImage;

    private void Start()
    {
        image = GetComponent<Image>();
        id=name;
    }

    public void SetItem()
    {
        CaldronManager instance=CaldronManager.instance;
        if (instance.CheckObject()!=instance.selectedItem)
        {
            item = instance.selectedItem;
            image.sprite = item.image;
        }else if (instance.CheckObject() == instance.selectedItem)
        {
            image.sprite = NullImage;
        }
    }


}
