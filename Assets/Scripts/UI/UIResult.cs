using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UIResult : MonoBehaviour
{
    public static UIResult Instance { get; internal set; }

    [Header("UI Elements")]
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _resultPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowResult(Result result)
    {
        GameObject resultObject = Instantiate(_resultPrefab, _resultPanel.transform);

        ResultComponent resultComponent = resultObject.GetComponent<ResultComponent>();

        for (int i = 0; i < result.items.Count; i++)
        {
            resultComponent.ingredients[i].sprite = result.items[i].itemData.image;
            resultComponent.preparetions[i].sprite = result.items[i].preparation.image;
            resultComponent.backgrounds[i].color = result.color[i];
            resultComponent.text.text = result.text;
        }
    }
}

[System.Serializable]
public class Result
{
    public List<CraftedIngredient> items = new();
    public List<Color> color = new();
    public string text;

    public Result(List<CraftedIngredient> item, List<Color> color, string text)
    {
        this.items = item;
        this.color = color;
        this.text = text;
    }
}
