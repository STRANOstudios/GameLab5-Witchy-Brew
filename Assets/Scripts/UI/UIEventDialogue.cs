using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UI Event Dialogue", menuName = "ScriptableObjects/UIEventDialogue")]
public class UIEventDialogue : ScriptableObject
{
    public UIEventName eventName;
    public List<UIDialogue> dialogueList;
}

public enum UIAnimationName
{
    Happy,
    Sad,
    Neutral,
    Explosion,
    None
}

public enum UIEventName
{
    Tutorial
}

[System.Serializable]
public class UIDialogue
{
    public string dialogueText;
    public UIAnimationName animationName;
}