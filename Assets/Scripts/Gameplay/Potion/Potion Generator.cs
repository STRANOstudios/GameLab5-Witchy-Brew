using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{
    int[,] ingredients = new int[5, 2];
    Color[] check = new Color[5];

    int attemptIndex;
    List<int> ingredientList = new List<int>();
    List<CauldronSlot> list = new List<CauldronSlot>();

    public delegate void Event(int id);
    public static event Event TutorialTask14 = null;

    private void GeneratePotion()
    {
        ingredientList.Clear();
        for (int i = 1; i < 13; i++)
        {

            ingredientList.Add(i);
        }
        for (int i = 0; i < ingredients.GetLength(0); i++)
        {
            int index = Random.Range(0, ingredientList.Count);
            ingredients[i, 0] = ingredientList[index];
            ingredientList.RemoveAt(index);
            ingredients[i, 1] = Random.Range(0, 4);
            Debug.Log(ingredients[i, 0] + " + " + ingredients[i, 1]);
        }
    }
    private void Start()
    {
        GeneratePotion();
        Debug.Log(NameGenerator.instance.GenerateName());
        list = CaldronManager.instance.slotList;
    }

    public void Confirm()
    {
        if (TutorialManager.TutorialIsRunning)
        {
            if (TutorialManager.taskIndex == 13 && !TutorialManager.TaskIsRunning[13]) TutorialTask14?.Invoke(14);
        }

        for (int i = 0; i < ingredients.GetLength(0); i++)
        {
            if (ingredients[i, 0] == list[i].item.itemData.id && ingredients[i, 1] == list[i].item.preparation.id)
            {
                check[i] = Color.green;
            }
            else if (ingredients[i, 0] == list[i].item.itemData.id && ingredients[i, 1] != list[i].item.preparation.id)
            {
                check[i] = Color.yellow;
            }
            else if (CheckItem(list[i].item.itemData.id, i))
            {
                check[i] = Color.blue;
            }
            else if (CheckPreparation(list[i].item.itemData.id, i))
            {
                check[i] = new Color(1f, 127 / 255f, 39 / 255f);
            }
            else
            {
                check[i] = Color.red;
            }
        }

        List<Color> colorList = new List<Color>();
        List<CraftedIngredient> craftedIngredientList = new List<CraftedIngredient>();

        for (int i = 0; i < check.Length; i++)
        {
            colorList.Add(check[i]);
            craftedIngredientList.Add(list[i].item);
        }

        attemptIndex++;

        UIResult.Instance.ShowResult(new Result(craftedIngredientList, colorList, "Attempt " + attemptIndex + " of 5"));

        for (int i = 0; i < check.Length; i++)
        {
            if (check[i] != Color.green) return;
        }

        // Win

        if (TutorialManager.TutorialIsRunning)
        {
            if (TutorialManager.taskIndex == 14 && !TutorialManager.TaskIsRunning[14]) TutorialTask14?.Invoke(15);
        }
    }

    private bool CheckPreparation(int ingredient, int index)
    {
        for (int i = 0; i < ingredients.GetLength(0); i++)
        {
            if (ingredient == ingredients[i, 0] && ingredients[i, 1] == list[index].item.preparation.id)
            {

                return true;
            }
        }
        return false;
    }

    private bool CheckItem(int ingredient, int index)
    {
        for (int i = 0; i < ingredients.GetLength(0); i++)
        {
            if (ingredient == ingredients[i, 0] && ingredients[i, 1] != list[index].item.preparation.id)
            {

                return true;
            }
        }
        return false;
    }


}
