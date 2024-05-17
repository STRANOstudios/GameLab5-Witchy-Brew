using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaldronManager : MonoBehaviour
{

    [SerializeField]Image image;
    public ItemData selectedItem;
    public static int cauldronSize=3;
    [SerializeField]GameObject slot;
    [SerializeField]Canvas canvas;
    public static CaldronManager instance;
    
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
    public ItemData CheckObject()
    {
        CauldronSlot[] slot = canvas.GetComponentsInChildren<CauldronSlot>();
        for (int i = 0; i <cauldronSize ; i++)
        {
            Debug.Log(slot[i].name);
            if (slot[i].item==selectedItem)
            {
                return slot[i].item;
            }
        }
        return null;
    }


}
    





