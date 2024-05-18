using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] CraftedIngredient NullItem;
    [SerializeField]Preparation preparation;
    [SerializeField]Image image;

    public void OnMouseDown()
    {
       CaldronManager instance=CaldronManager.instance;
        if(instance.selectedItem!=NullItem)
        {
            instance.SetPreparation(this.preparation);
        }
    }

}
