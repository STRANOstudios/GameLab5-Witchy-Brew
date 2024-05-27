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
            if (TutorialManager.TaskIsRunning[1]) return;

            if (TutorialManager.taskIndex == 3 && !TutorialManager.TaskIsRunning[3]) TutorialTask1?.Invoke(4);
        }

        selectedItem = item;
        //preparationImage.sprite = _item.preparation.image;
        //itemImage.sprite = _item.itemData.image;

        if (item.itemData.id != 0) DialogueManager.Instance.ShowEvent(item);

        if (TutorialManager.taskIndex == 1) TutorialTask1?.Invoke(2);
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






