using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{

    [SerializeField] ItemData item;
    [SerializeField]TMP_Text text;
    public delegate void Click(ItemData item);
    public static event Click OnClicked;

    private void Awake()
    {
        text.text = item.nome;
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
