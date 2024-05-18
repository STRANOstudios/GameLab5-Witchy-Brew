using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Ingredient;

public class Ingredient : MonoBehaviour
{

    [SerializeField] CraftedIngredient item;

    [SerializeField]TMP_Text text;
    public delegate void Click(CraftedIngredient item);
    public static event Click OnClicked;

    private void Awake()
    {
        text.text = item.itemData.nome;
    }

    private void OnMouseDown()
    {
        OnClicked(this.item);
    }
    public void OnMouseEnter()
    {
        text.gameObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        text.gameObject.SetActive(false);
    }
}
