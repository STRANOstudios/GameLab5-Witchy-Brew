using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaldronManager : MonoBehaviour
{

    [SerializeField]Image itemImage;
    [SerializeField] Image preparationImage;
    public CraftedIngredient selectedItem=null;
    public static int cauldronSize=3;
    [SerializeField]GameObject slot;
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
        CreateSlot(cauldronSize);
        if (instance == null) instance = this;
    }
    public void CreateSlot(int index)
    {
        for (int i = 0; i < index; i++) {
            GameObject instance=Instantiate(slot,canvas.transform);
            instance.transform.position += Vector3.right * i;
            instance.name=i.ToString();
        }
    }
}
    





