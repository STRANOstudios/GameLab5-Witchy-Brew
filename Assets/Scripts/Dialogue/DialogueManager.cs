using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _item;
    [SerializeField] GameObject ballon;
    [SerializeField] GameObject character;

    [Header("Events")]
    [SerializeField] UIEventDialogue Idle;
    [SerializeField] UIEventDialogue Holding;
    [SerializeField] UIEventDialogue Success;
    [SerializeField] UIEventDialogue Failure;
    // Dialogue
    [SerializeField] UIEventDialogue Tutorial;
    [SerializeField] List<UIEventDialogue> ClientArrived = new();
    [SerializeField] List<UIEventDialogue> ClientLeaving = new();
    [SerializeField] List<UIEventDialogue> ChoosingIngredients = new();
    [SerializeField] UIEventDialogue ProcessingIngredients;
    [SerializeField] UIEventDialogue PotionFiled;
    [SerializeField] UIEventDialogue PotionReady;
    [SerializeField] AnimationClip ClientAsk;

    public enum STATE
    {
        IDLE,
        HOLDING,
        SUCCESS,
        FAILURE,
        DIALOGUE
    }

    public enum DIALOGUETYPE
    {
        TUTORIAL,
        CLIENTARRIVED,
        CLIENTLEAVING,
        CHOOSINGINGREDIENTS,
        PROCESSINGINGREDIENTS,
        POTIONFILED,
        POTIONREADY
    }

    private STATE currentState;

    private AnimManager animManager;
    private WriterText writerText;
    private ResultComponent resultComponent;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animManager = character.GetComponent<AnimManager>();
        writerText = ballon.GetComponent<WriterText>();
        resultComponent = _item.GetComponent<ResultComponent>();

        ShowEvent(STATE.IDLE);
    }

    private void OnEnable()
    {
        WriterText.Finished += ResetShow;
    }

    private void OnDisable()
    {
        WriterText.Finished -= ResetShow;
    }

    /// <summary>
    /// Shows an event in the UI
    /// </summary>
    /// <param name="state"></param>
    public virtual void ShowEvent(STATE state)
    {
        ResetShow();

        switch (state)
        {
            case STATE.IDLE:
                animManager.Play(ExtractElements(Idle, true));
                break;
            case STATE.SUCCESS:
                animManager.Play(ExtractElements(Success, true));
                break;
            case STATE.FAILURE:
                animManager.Play(ExtractElements(Failure, true));
                break;
        }

        currentState = state;
    }

    /// <summary>
    /// Shows an event in the UI
    /// </summary>
    /// <param name="item"></param>
    public virtual void ShowEvent(CraftedIngredient item)
    {
        if (!item) return;

        _item.gameObject.SetActive(true);

        resultComponent.ingredients[0].sprite = item.itemData.image;
        resultComponent.preparetions[0].sprite = item.preparation.image;

        animManager.Play(ExtractElements(Holding, true));

        currentState = STATE.HOLDING;
    }

    /// <summary>
    /// Shows an event in the UI
    /// </summary>
    /// <param name="state"></param>
    /// <param name="type"></param>
    public virtual void ShowEvent(STATE state, DIALOGUETYPE type)
    {
        ballon.SetActive(true);

        switch (type)
        {
            case DIALOGUETYPE.TUTORIAL:
                animManager.Play(ExtractElements(Tutorial, true));
                break;
            case DIALOGUETYPE.CLIENTARRIVED:
                animManager.Play(ExtractElements(ClientArrived[Random.Range(0, ClientArrived.Count)], true));
                break;
            case DIALOGUETYPE.CLIENTLEAVING:
                animManager.Play(ExtractElements(ClientLeaving[Random.Range(0, ClientLeaving.Count)], true));
                break;
            case DIALOGUETYPE.CHOOSINGINGREDIENTS:
                animManager.Play(ExtractElements(ChoosingIngredients[Random.Range(0, ChoosingIngredients.Count)], true));
                break;
            case DIALOGUETYPE.PROCESSINGINGREDIENTS:
                animManager.Play(ExtractElements(ProcessingIngredients, true));
                break;
            case DIALOGUETYPE.POTIONFILED:
                animManager.Play(ExtractElements(PotionFiled, true));
                break;
        }
        animManager.Play(ExtractElements(Holding, true));

        currentState = state;
    }

    /// <summary>
    /// Extracts elements from UIEventDialogue
    /// </summary>
    /// <param name="eventDialogue"></param>
    /// <param name="extractAnimationNames"></param>
    /// <returns></returns>
    private List<string> ExtractElements(UIEventDialogue eventDialogue, bool extractAnimationNames = false)
    {
        return eventDialogue.dialogueList.Select(tmp => extractAnimationNames ? tmp.animationName.name : tmp.dialogueText).ToList();
    }

    private void ResetShow()
    {
        _item.SetActive(false);
        ballon.SetActive(false);
        character.SetActive(true);
    }
}