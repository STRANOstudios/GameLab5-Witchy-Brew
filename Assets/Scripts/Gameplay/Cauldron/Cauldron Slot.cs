using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CauldronSlot : MonoBehaviour
{
    [SerializeField] CraftedIngredient NullItem;
    [SerializeField] Image itemImage;
    [SerializeField] Image preparationImage;

    public CraftedIngredient item;
    private string id;

    private bool isPressed = false;

    public delegate void Event(int id);
    public static event Event TutorialTask2 = null;

    private void Start()
    {
        item = NullItem;
        //itemImage = GetComponent<Image>();
        itemImage.sprite = NullItem.itemData.image;
        //preparationImage = GetComponentInChildren<Image>();
        preparationImage.sprite = NullItem.preparation.image;
        id = name;
    }

    public void SetItem()
    {
        if (TutorialManager.TutorialIsRunning)
        {
            if (TutorialManager.TaskIsRunning[2]) return;

            if (TutorialManager.taskIndex == 5 && !TutorialManager.TaskIsRunning[5]) TutorialTask2?.Invoke(6);
            if (TutorialManager.taskIndex == 8 && !TutorialManager.TaskIsRunning[8]) TutorialTask2?.Invoke(9);
            if (TutorialManager.taskIndex == 12 && !TutorialManager.TaskIsRunning[12]) TutorialTask2?.Invoke(13);
        }

        if (isPressed) return;
        StartCoroutine(ButtonPressed());

        CaldronManager instance = CaldronManager.instance;
        CraftedIngredient temp = instance.selectedItem;
        instance.ChangeItem(this.item);

        if (item.itemData.id == 0) DialogueManager.Instance.ShowEvent(DialogueManager.STATE.IDLE);
        else DialogueManager.Instance.ShowEvent(item);

        item = temp;
        itemImage.sprite = temp.itemData.image;
        preparationImage.sprite = temp.preparation.image;
        instance.CheckSlots();

        if (TutorialManager.taskIndex == 2) TutorialTask2?.Invoke(3);
        if (TutorialManager.taskIndex == 6) TutorialTask2?.Invoke(7);
        if (TutorialManager.taskIndex == 9) TutorialTask2?.Invoke(10);
        if (TutorialManager.taskIndex == 10) TutorialTask2?.Invoke(11);
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
            this.item = NullItem;
            this.preparationImage.sprite = NullItem.preparation.image;
            this.itemImage.sprite = NullItem.itemData.image;
            CaldronManager.instance.CheckSlots();
        }
    }

    private IEnumerator ButtonPressed(float delay = 0.3f)
    {
        isPressed = true;
        yield return new WaitForSecondsRealtime(delay);
        isPressed = false;
    }

}
