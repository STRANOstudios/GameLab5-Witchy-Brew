using System.Collections;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(AudioSource))]
public class UIDialogueManager : MonoBehaviour
{
    [Header("Dialogue Reference")]
    [SerializeField] private TMP_Text text;

    [Header("Audio Reference")]
    [SerializeField] private AudioClip audioClip;

    [Header("Animation Reference")]
    [SerializeField] private Transform meshTransform;
    [SerializeField] private Transform targetPosition;
    [Space]
    [SerializeField] private ResultComponent UIItem;

    [Header("UI Settings")]
    [SerializeField, Min(0)] private float timePerCharacter = 0.05f;
    [SerializeField, Min(0)] private float dialogueDelay = 0.5f;
    [SerializeField, Min(0)] private float fadeDelay = 0.5f;
    [SerializeField, Min(0)] private float fadeDuration = 0.5f;
    [SerializeField, Min(0)] private float animationDuration = 0.5f;

    private AudioSource audioSource;
    private Animator animator;

    private int currentDialogue = 0;
    private UIEventDialogue currentEventDialogue;
    private string currentAnimationName;

    private UIDialogueState dialogueState = UIDialogueState.None;

    public static UIDialogueManager Instance { get; internal set; }

    public delegate void UIDialogueManagerState();
    public static event UIDialogueManagerState OnDialogueStarted;
    public static event UIDialogueManagerState OnDialogueFinished;
    public static event UIDialogueManagerState OnDialogueHolding;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        animator = meshTransform.gameObject.GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Starts the dialogue
    /// Prepares the text and starts the animation
    /// </summary>
    /// <param name="eventDialogue"></param>
    public virtual void StartDialogue(UIEventDialogue eventDialogue)
    {
        OnDialogueStarted?.Invoke();

        UIItem.gameObject.SetActive(false);

        StopAllCoroutines();
        currentDialogue = 0;
        currentEventDialogue = eventDialogue;
        StartCoroutine(WriteText(currentEventDialogue));
        if (!meshTransform) return;
        MoveToTarget(meshTransform, targetPosition.position, animationDuration, true);
    }

    /// <summary>
    /// Starts the dialogue with an animation clip
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="clip"></param>
    public virtual void StartDialogue(string dialogue, AnimationClip clip)
    {
        OnDialogueStarted?.Invoke();

        UIItem.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(WriteText(currentEventDialogue));
        if (!meshTransform) return;
        MoveToTarget(meshTransform, targetPosition.position, animationDuration, true);
    }

    /// <summary>
    /// Holding an obejct in the inventory
    /// </summary>
    /// <param name="eventDialogue"></param>
    /// <param name="item"></param>
    public void HoldingObject(UIEventDialogue eventDialogue, CraftedIngredient item)
    {
        OnDialogueHolding?.Invoke();

        StopAllCoroutines();
        currentDialogue = 0;
        currentEventDialogue = eventDialogue;

        if (!meshTransform) return;
        MoveToTarget(meshTransform, targetPosition.position, animationDuration, true);

        Animation();

        UIItem.gameObject.SetActive(true);

        UIItem.ingredients[0].sprite = item.itemData.image;
        UIItem.preparetions[0].sprite = item.preparation.image;
    }

    private IEnumerator WriteText(UIEventDialogue eventDialogue, bool delay = true)
    {
        dialogueState = UIDialogueState.Playing;

        while (currentDialogue < eventDialogue.dialogueList.Count)
        {
            if (audioClip) audioSource.PlayOneShot(audioClip);
            Animation();

            text.text = "";
            foreach (char c in eventDialogue.dialogueList[currentDialogue].dialogueText)
            {
                text.text += c;
                yield return new WaitForSeconds(timePerCharacter);
            }

            dialogueState = UIDialogueState.Finished;

            currentDialogue++;
            if (delay) yield return new WaitForSeconds(dialogueDelay);
            if (currentDialogue < eventDialogue.dialogueList.Count)
            {
                // Reset to the beginning or handle end of dialogue logic
                dialogueState = UIDialogueState.None;
                OnDialogueEnd();
            }
        }
    }

    private void NextDialogue()
    {
        if (currentEventDialogue == null) return;

        currentDialogue++;

        if (currentDialogue < currentEventDialogue.dialogueList.Count)
        {
            StartCoroutine(WriteText(currentEventDialogue));
        }
        else
        {
            currentDialogue = 0;  // Reset to the beginning or handle end of dialogue logic
            dialogueState = UIDialogueState.None;
            OnDialogueEnd();
        }
    }

    private void Animation()
    {
        if (!animator) return;

        if (currentAnimationName == currentEventDialogue.dialogueList[currentDialogue].animationName.name) return;

        currentAnimationName = currentEventDialogue.dialogueList[currentDialogue].animationName.name;

        animator.CrossFade(currentAnimationName, fadeDelay);
    }

    /// <summary>
    /// When the button is pressed and the dialogue is playing, write all the text, else play the next dialogue
    /// </summary>
    public void ButtonPressed()
    {
        if (dialogueState == UIDialogueState.Playing)
        {
            StopAllCoroutines();
            text.text = currentEventDialogue.dialogueList[currentDialogue].dialogueText;  // Complete the current dialogue instantly
            dialogueState = UIDialogueState.Finished;
        }
        else if (dialogueState != UIDialogueState.None)
        {
            NextDialogue();
        }
    }

    private void OnDialogueEnd()
    {
        StartCoroutine(FadeOut(meshTransform, fadeDuration));
    }

    private void MoveToTarget(Transform obj, Vector3 target, float duration, bool fadeIn)
    {
        StartCoroutine(MoveToTargetCoroutine(obj, target, duration, fadeIn));
    }

    private IEnumerator MoveToTargetCoroutine(Transform obj, Vector3 target, float duration, bool fadeIn)
    {
        Vector3 startPos = obj.position;
        float elapsed = 0f;
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (!canvasGroup)
        {
            canvasGroup = obj.gameObject.AddComponent<CanvasGroup>();
        }

        while (elapsed < duration)
        {
            obj.position = Vector3.Lerp(startPos, target, elapsed / duration);
            if (fadeIn)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            }
            else
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.position = target;
        if (fadeIn)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

    private IEnumerator FadeOut(Transform obj, float duration)
    {
        float elapsed = 0f;
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (!canvasGroup)
        {
            canvasGroup = obj.gameObject.AddComponent<CanvasGroup>();
        }

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;

        OnDialogueFinished?.Invoke();
    }
}

public enum UIDialogueState
{
    Playing,
    Finished,
    None
}