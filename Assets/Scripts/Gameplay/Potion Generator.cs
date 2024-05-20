using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{
    CraftedIngredient[] ingredients=new CraftedIngredient[5];
    [SerializeField]List<ItemData> itemDatas = new List<ItemData>();
    [SerializeField] List<Preparation> preparations = new List<Preparation>();
    List<ItemData> ingredientList;

    private void GeneratePotion()
    {
        ingredientList = itemDatas;
        for(int i = 0; i < ingredients.Length; i++)
        {
            CraftedIngredient slot=new CraftedIngredient();
            int random = Random.Range(0, 12);
            slot.itemData = ingredientList[Random.Range(0,12)];

            slot.preparation = preparations[Random.Range(0,4)];
            ingredients[i] = slot;
        }

    }
    private void Start()
    {
        GeneratePotion();
    }



}
