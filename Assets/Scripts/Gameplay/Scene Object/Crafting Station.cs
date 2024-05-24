using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] CraftedIngredient NullItem;
    [SerializeField] Preparation preparation;
    [SerializeField] Image image;

    public void OnMouseDown()
    {
        CaldronManager instance = CaldronManager.instance;
        if (instance.selectedItem != NullItem)
        {
            instance.SetPreparation(this.preparation);
        }
    }

}
