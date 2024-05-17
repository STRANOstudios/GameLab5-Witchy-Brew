using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _dialogueBalloon;
    [SerializeField] private GameObject _characterPortrait;

    [SerializeField] private GameObject _attemptBoard;

    [SerializeField] private UIEventDialogue EventDialogue;

    private void OnEnable()
    {
        UIDialogueManager.OnDialogueFinished += ResetUIDialogue;
    }

    private void OnDisable()
    {
        UIDialogueManager.OnDialogueFinished -= ResetUIDialogue;
    }

    public void Button()
    {
        if (!_dialogueBalloon || !_characterPortrait) return;

        _dialogueBalloon.SetActive(true);
        _characterPortrait.SetActive(true);

        UIDialogueManager.Instance.StartDialogue(EventDialogue);
    }

    public void ResetUIDialogue()
    {
        _dialogueBalloon.SetActive(false);
        _characterPortrait.SetActive(false);
    }
}
