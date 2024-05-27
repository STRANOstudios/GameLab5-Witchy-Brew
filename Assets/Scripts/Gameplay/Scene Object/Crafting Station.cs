using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] CraftedIngredient NullItem;
    [SerializeField] Preparation preparation;
    [SerializeField] Image image;

    public delegate void Event(int id);
    public static event Event TutorialTask5 = null;

    public void OnMouseDown()
    {
        if (TutorialManager.TutorialIsRunning)
        {
            if (TutorialManager.TaskIsRunning[4] && preparation.name == "Cooked") return;
            if (TutorialManager.TaskIsRunning[7] && preparation.name == "Enchanted") return;
            if (TutorialManager.TaskIsRunning[11] && preparation.name == "Grounded") return;
        }

        CaldronManager instance = CaldronManager.instance;
        if (instance.selectedItem != NullItem)
        {
            instance.SetPreparation(this.preparation);
        }

        if (TutorialManager.taskIndex == 4) TutorialTask5?.Invoke(5);
        if (TutorialManager.taskIndex == 7) TutorialTask5?.Invoke(8);
        if (TutorialManager.taskIndex == 11) TutorialTask5?.Invoke(12);
    }

}
