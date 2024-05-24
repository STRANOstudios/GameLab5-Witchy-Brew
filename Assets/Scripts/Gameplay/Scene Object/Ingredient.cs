using TMPro;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    [SerializeField] CraftedIngredient item;
    [SerializeField] Preparation preparation;
    [SerializeField] TMP_Text text;
    [SerializeField] Canvas canvas;

    public delegate void Click(CraftedIngredient item);
    public static event Click OnClicked;

    private void Awake()
    {
        text.text = item.itemData.nome;
    }

    private void OnMouseDown()
    {
        item.preparation = preparation;
        OnClicked(this.item);
    }

    public void OnMouseEnter()
    {
        canvas.gameObject.SetActive(true);
//        text.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        canvas.gameObject.SetActive(false);
//        text.gameObject.SetActive(false);
    }
}
