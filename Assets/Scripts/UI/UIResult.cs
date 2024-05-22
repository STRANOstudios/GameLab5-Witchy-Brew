using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class UIResult : MonoBehaviour
{
    public static UIResult Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _resultPrefab;

    private readonly List<GameObject> resultObjects = new();

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Shows the result in the UI
    /// Instantiates the prefab and adds it to the list
    /// Sets the properties of the prefab
    /// </summary>
    /// <param name="result"></param>
    public void ShowResult(Result result)
    {
        // Instantiate the prefab and add it to the list
        GameObject resultObject = Instantiate(_resultPrefab, _resultPanel.transform);
        resultObjects.Add(resultObject);

        ResultComponent resultComponent = resultObject.GetComponent<ResultComponent>();
        int itemCount = result.Items.Count;

        // Cache the references to the lists only once
        var ingredients = resultComponent.ingredients;
        var preparations = resultComponent.preparetions;
        var backgrounds = resultComponent.backgrounds;

        // Iterate through the items and set the corresponding properties
        for (int i = 0; i < itemCount; i++)
        {
            ingredients[i].sprite = result.Items[i].itemData.image;
            preparations[i].sprite = result.Items[i].preparation.image;
            backgrounds[i].color = result.Colors[i];
        }

        resultComponent.text.text = result.Text;
    }

    /// <summary>
    /// Removes all result objects
    /// Prepares the list for the next use
    /// </summary>
    public void RemoveResult()
    {
        for (int i = 0; i < resultObjects.Count; i++)
        {
            Destroy(resultObjects[i]);
        }
        resultObjects.Clear();
    }
}

[System.Serializable]
public class Result
{
    public List<CraftedIngredient> Items { get; private set; }
    public List<Color> Colors { get; private set; }
    public string Text { get; private set; }

    /// <summary>
    /// Creates a new instance of Result
    /// </summary>
    /// <param name="items"></param>
    /// <param name="colors"></param>
    /// <param name="text"></param>
    public Result(List<CraftedIngredient> items, List<Color> colors, string text)
    {
        Items = items ?? new List<CraftedIngredient>(); // Ensure items is not null
        Colors = colors ?? new List<Color>(); // Ensure colors is not null
        Text = text ?? string.Empty; // Ensure text is not null
    }
}
