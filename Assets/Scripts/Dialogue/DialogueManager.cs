using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _item;
    [SerializeField] GameObject baloon;
    [SerializeField] GameObject character;
    [SerializeField] GameObject skipTutorialBtn;

    [Header("Events")]
    [SerializeField] UIEventDialogue Idle;
    [SerializeField] UIEventDialogue Holding;
    [SerializeField] UIEventDialogue Success;
    [SerializeField] UIEventDialogue Failure;
    [SerializeField] UIEventDialogue Enchanting;
    [SerializeField] UIEventDialogue Grounding;
    [SerializeField] UIEventDialogue Cooking;
    // Dialogue
    [SerializeField] List<UIEventDialogue> ClientArrived = new();
    [SerializeField] List<UIEventDialogue> ClientLeaving = new();
    [SerializeField] List<UIEventDialogue> ChoosingIngredients = new();
    [SerializeField] UIEventDialogue ProcessingIngredients;
    [SerializeField] UIEventDialogue PotionFiled;
    [SerializeField] UIEventDialogue PotionReady;
    [SerializeField] AnimationClip ClientAsk;

    [Header("Settings")]
    [SerializeField, Min(0)] float timePerCharacter = 0.08f;
    [SerializeField, Min(0)] float lineDuration = 3f;
    [SerializeField, Min(0)] float fadeDelay = 0.1f;

    public enum STATE
    {
        IDLE,
        HOLDING,
        PREPARATION,
        SUCCESS,
        FAILURE,
        DIALOGUE,
        TUTORIAL
    }

    public enum DIALOGUETYPE
    {
        CLIENTARRIVED,
        CLIENTLEAVING,
        CHOOSINGINGREDIENTS,
        PROCESSINGINGREDIENTS,
        POTIONFILED,
        POTIONREADY
    }

    public enum PREPARATION
    {
        NONE,
        ENCHANTING,
        GROUNDING,
        COOKING
    }

    private STATE currentState;

    private AnimManager animManager;
    private WriterText writerText;
    private ResultComponent resultComponent;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        animManager = character.GetComponent<AnimManager>();
        writerText = baloon.GetComponentInChildren<WriterText>();
        resultComponent = _item.GetComponent<ResultComponent>();
    }

    private void Start()
    {
        //ShowEvent(STATE.IDLE);
    }

    private void OnEnable()
    {
        WriterText.Finished += ResetShow;
        TutorialManager.Finished += ResetShow;
    }

    private void OnDisable()
    {
        WriterText.Finished -= ResetShow;
        TutorialManager.Finished -= ResetShow;
    }

    /// <summary>
    /// Shows an event in the UI
    /// </summary>
    /// <param name="state"></param>
    public virtual void ShowEvent(STATE state)
    {
        if (state != STATE.TUTORIAL) ResetShow();

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
    public virtual void ShowEvent(CraftedIngredient item, PREPARATION preparation = PREPARATION.NONE)
    {
        if (!item) return;

        if (preparation != PREPARATION.NONE)
        {
            StartCoroutine(Preparation(item, preparation));
            return;
        }

        //_item.SetActive(true);
        CrossFade(_item, true);

        resultComponent.ingredients[0].sprite = item.itemData.image;
        resultComponent.preparetions[0].sprite = item.preparation.image;

        animManager.Play(ExtractElements(Holding, true));

        currentState = STATE.HOLDING;
    }

    private IEnumerator Preparation(CraftedIngredient item, PREPARATION preparation)
    {

        switch (preparation)
        {
            case PREPARATION.ENCHANTING:
                animManager.Play(ExtractElements(Enchanting, true));
                break;
            case PREPARATION.GROUNDING:
                animManager.Play(ExtractElements(Grounding, true));
                break;
            case PREPARATION.COOKING:
                animManager.Play(ExtractElements(Cooking, true));
                break;
        }

        yield return new WaitForSeconds(0.3f);

        //_item.SetActive(true);
        CrossFade(_item, true);

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
        baloon.SetActive(true);

        switch (type)
        {
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

        currentState = state;
    }

    /// <summary>
    /// Shows an event in the UI
    /// </summary>
    /// <param name="state"></param>
    /// <param name="eventDialogue"></param>
    public virtual void ShowEvent(STATE state, UIEventDialogue eventDialogue)
    {
        if (state == STATE.TUTORIAL) skipTutorialBtn.SetActive(true);

        _item.GetComponent<CanvasGroup>().alpha = 0;

        baloon.SetActive(true);

        writerText.Write(ExtractElements(eventDialogue, false));
        animManager.Play(ExtractElements(eventDialogue, true));

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
        CrossFade(_item, 0);

        baloon.SetActive(false);
        character.SetActive(true);
        animManager.Play(ExtractElements(Idle, true));

        skipTutorialBtn.SetActive(false);
    }

    protected virtual void CrossFade(GameObject gameObject, bool target = false)
    {
        StartCoroutine(FadeCoroutine(gameObject.GetComponent<CanvasGroup>(), target));
    }

    protected virtual void CrossFade(GameObject gameObject, float target)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = target;
    }

    private IEnumerator FadeCoroutine(CanvasGroup canvasGroup, bool target = false)
    {
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = target ? 1f : startAlpha < 0.5f ? 1 : 0;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDelay)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDelay);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    public float FadeDelay => fadeDelay;
    public float TimePerCharacter => timePerCharacter;
    public float LineDuration => lineDuration;

}