using UnityEngine;
using UnityEngine.UI;

public class CaldronManager : MonoBehaviour
{
    Image thisimage;
    [SerializeField]Image saveImage;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        thisimage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        thisimage.color = saveImage.color;
    }


}
