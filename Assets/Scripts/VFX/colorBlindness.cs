using UnityEngine;

[CreateAssetMenu(fileName = "ColorBlindness", menuName = "ColorBlindness", order = 1)]
public class ColorBlindness : ScriptableObject
{
    public ChannelData redChannel;
    public ChannelData greenChannel;
    public ChannelData blueChannel;
}

[System.Serializable]
public class ChannelData
{
    [SerializeField, Range(0,100f)] float red = 0.0f;
    [SerializeField, Range(0, 100f)] float green = 0.0f;
    [SerializeField, Range(0, 100f)] float blue = 0.0f;

    public float Red => red;
    public float Green => green;
    public float Blue => blue;
}