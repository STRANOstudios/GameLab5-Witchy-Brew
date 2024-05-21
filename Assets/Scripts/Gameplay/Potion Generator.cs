using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : MonoBehaviour
{
    [SerializeField]int[,] ingredients=new int[5,2];
    List<int> ingredientList=new List<int>();   


    private void GeneratePotion()
    {
        ingredientList.Clear();
        for(int i=1; i<13;i++)
        {
            ingredientList.Add(i);
        }
        for(int i = 0; i < ingredients.GetLength(0); i++)
        {
            int index=Random.Range(0,ingredientList.Count);
            ingredients[i,0] = ingredientList[index];
            ingredientList.RemoveAt(index);
            ingredients[i, 1] = Random.Range(0,4);
            Debug.Log(ingredients[i, 0]+ " + "+ ingredients[i,1]);
        }

    }
    private void Start()
    {
        GeneratePotion();
    }



}
