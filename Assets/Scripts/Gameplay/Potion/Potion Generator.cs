using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionGenerator : MonoBehaviour
{
    int[,] ingredients = new int[5, 2];
    Color[] check = new Color[5];

    int attemptIndex;
    List<int> ingredientList = new List<int>();
    List<CauldronSlot> list = new List<CauldronSlot>();

    public Color correctColor, wrongPreparationColor, wrongPositionColor,wrongPositionAndPreparationColor, ingredientNotPresentColor;

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
        for (int i = 0; i < ingredients.GetLength(0); i++)
        {
            if (ingredients[i, 0] == list[i].item.itemData.id && ingredients[i, 1] == list[i].item.preparation.id)
            {
                check[i] = correctColor;
            }
            else if (ingredients[i, 0] == list[i].item.itemData.id && ingredients[i, 1] != list[i].item.preparation.id)
            {
                check[i] = wrongPreparationColor;
            }
            else if (CheckItem(list[i].item.itemData.id, i))
            {
                check[i] = wrongPositionAndPreparationColor;
            }
            else if (CheckPreparation(list[i].item.itemData.id, i))
            {
                check[i] = wrongPositionColor;
            }
            else
            {
                check[i] = ingredientNotPresentColor;
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
    }

    private bool CheckPreparation(int ingredient,int index)
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
