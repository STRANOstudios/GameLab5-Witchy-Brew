using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] UIEventDialogue Tutorial;
    [SerializeField] List<UIEventDialogue> ClientArrived = new();
    [SerializeField] List<UIEventDialogue> ClientLeaving = new();
    [SerializeField] List<UIEventDialogue> ChoosingIngredients = new();
    [SerializeField] UIEventDialogue TakingIngredient;
    [SerializeField] UIEventDialogue ProcessingIngredients;
    [SerializeField] UIEventDialogue PotionFiled;
    [SerializeField] UIEventDialogue PotionReady;
    [SerializeField] AnimationClip ClientAsk;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Shows an event
    /// </summary>
    /// <param name="type"></param>
    /// <param name="item"></param>
    public virtual void ShowEvent(DialogueType type, CraftedIngredient item = null)
    {
        if (item != null)
        {
            UIDialogueManager.Instance.HoldingObject(TakingIngredient, item);
            return;
        }

        UIEventDialogue eventDialogue = null;

        switch (type)
        {
            case DialogueType.Tutorial:
                eventDialogue = Tutorial;
                break;
            case DialogueType.ClientArrived:
                eventDialogue = ClientArrived[Random.Range(0, ClientArrived.Count)];
                break;
            case DialogueType.ClientLeaving:
                eventDialogue = ClientLeaving[Random.Range(0, ClientLeaving.Count)];
                break;
            case DialogueType.ChoosingIngredients:
                eventDialogue = ChoosingIngredients[Random.Range(0, ChoosingIngredients.Count)];
                break;
            case DialogueType.ProcessingIngredients:
                eventDialogue = ProcessingIngredients;
                break;
        }

        UIDialogueManager.Instance.StartDialogue(eventDialogue);
    }

    public virtual void ShowEvent(string dialogue)
    {
        UIDialogueManager.Instance.StartDialogue(dialogue, ClientAsk);
    }
}

public enum DialogueType
{
    Tutorial,
    ClientArrived,
    ClientLeaving,
    ChoosingIngredients,
    TakingIngredient,
    ProcessingIngredients
}
