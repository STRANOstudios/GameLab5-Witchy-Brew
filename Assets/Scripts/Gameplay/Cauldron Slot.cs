using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronSlot : MonoBehaviour
{
    public ItemData item;
    private string id;
    [SerializeField]Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        id=name;
    }

    public void SetItem()
    {
        CaldronManager instance=CaldronManager.instance;
        if (!instance.CheckObject())
        {
            item = instance.selectedItem;
            image.sprite = item.image;
        }else if(instance.CheckObject()&&this.item!=instance.selectedItem){

        }
    }


}
