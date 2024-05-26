using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaldronManager : MonoBehaviour
{

    [SerializeField] Image itemImage;
    [SerializeField] Image preparationImage;
    public CraftedIngredient selectedItem = null;
    public List<CauldronSlot> slotList;
    public Button button;

    public delegate void Event(int id);
    public static event Event TutorialTask1 = null;

    [SerializeField] Canvas canvas;
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
        selectedItem.preparation = item;
        preparationImage.sprite = item.image;
    }

    public void ChangeItem(CraftedIngredient item)
    {
        if (TutorialManager.TutorialIsRunning)
        {
            Debug.Log("Tutorial is running");
            if (TutorialManager.TaskIsRunning1) return;
        }

        selectedItem = item;
        //preparationImage.sprite = _item.preparation.image;
        //itemImage.sprite = _item.itemData.image;

        if (item.itemData.id != 0) DialogueManager.Instance.ShowEvent(item);

        TutorialTask1?.Invoke(1);
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void CheckSlots()
    {
        foreach (var slot in slotList)
        {
            if (slot.item.itemData.id == 0)
            {
                button.interactable = false;
                return;
            }
        }
        button.interactable = true;
    }



}






