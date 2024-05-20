using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{
    CraftedIngredient[] ingredients=new CraftedIngredient[5];
    [SerializeField]List<ItemData> itemData = new List<ItemData>();

    private void Start()
    {
        for(int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i] = new CraftedIngredient();
        }
    }





}
