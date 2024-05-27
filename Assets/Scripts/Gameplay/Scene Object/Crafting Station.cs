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
            if (TutorialManager.TaskIsRunning[4]) return;
        }

        CaldronManager instance = CaldronManager.instance;
        if (instance.selectedItem != NullItem)
        {
            instance.SetPreparation(this.preparation);
        }

        if (TutorialManager.taskIndex == 4) TutorialTask5?.Invoke(5);
    }

}
