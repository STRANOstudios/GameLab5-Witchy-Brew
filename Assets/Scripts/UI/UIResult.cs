using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIResult : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _resultPrefab;

    [Header("References")]
    [SerializeField] private Sprite _red;
    [SerializeField] private Sprite _green;
    [SerializeField] private Sprite _yellow;
    [SerializeField] private Sprite _orange;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void ShowResult(Result result)
    {
        GameObject resultObject = Instantiate(_resultPrefab, _resultPanel.transform);

        for (int i = 0; i < result.sprites.Count; i++)
        {
            resultObject.transform.GetChild(i).GetComponent<Image>().sprite = result.sprites[i];

            Sprite color = null;
            switch (result.colorCodes[i])
            {
                case ColorCode.RED:

                    color = _red;
                    break;

                case ColorCode.GREEN:

                    color = _green;
                    break;

                case ColorCode.YELLOW:

                    color = _yellow;
                    break;

                case ColorCode.ORANGE:

                    color = _orange;
                    break;
            }
            resultObject.transform.GetChild(i).GetComponent<Image>().sprite = color;
        }
    }
}

[System.Serializable]
public class Result
{
    public List<Sprite> sprites = new();
    public List<ColorCode> colorCodes = new();

    public Result(List<Sprite> sprites, List<ColorCode> colorCodes)
    {
        this.sprites = sprites;
        this.colorCodes = colorCodes;
    }
}

public enum ColorCode
{
    RED,
    GREEN,
    YELLOW,
    ORANGE
}
