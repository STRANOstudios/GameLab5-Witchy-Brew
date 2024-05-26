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
            if (TutorialManager.TaskIsRunning[1]) return;
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
