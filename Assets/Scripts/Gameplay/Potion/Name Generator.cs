using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{


    public static NameGenerator instance;
    [SerializeField]List<string> color = new List<string>();
    [SerializeField]List<string> type = new List<string>();
    [SerializeField]List<string> adjective = new List<string>();
    [SerializeField]List<string> goal = new List<string>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public string GenerateName()
    {
        string potionName = "";
        int randomColor=Random.Range(0, color.Count);
        int randomType = Random.Range(0, type.Count);
        int randomAdjective = Random.Range(0, adjective.Count);
        int randomGoal= Random.Range(0, goal.Count);
        potionName = color[randomColor]+" "+type[randomType]+ " "+ adjective[randomAdjective]+" "+goal[randomGoal];
        return potionName;
    }





}
