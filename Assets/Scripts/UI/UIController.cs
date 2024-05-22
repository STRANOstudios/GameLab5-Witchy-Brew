using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _dialogueBalloon;
    [SerializeField] private GameObject _characterPortrait;

    [SerializeField] private UIEventDialogue EventDialogue;

    [Header("Menu Elements")]
    [SerializeField] private GameObject _menuInGame;
    [SerializeField] private GameObject _menuOptions;
    [SerializeField] private GameObject _menuOptionAudio;
    [SerializeField] private GameObject _menuOptionGraphics;

    private void OnEnable()
    {
        UIDialogueManager.OnDialogueFinished += ResetUIDialogue;
        UIDialogueManager.OnDialogueStarted += SetUIDialogue;
        UIDialogueManager.OnDialogueHolding += HoldingUIDialogue;

        if (GameManager.Instance) GameManager.Instance.Pause += OnPause;
    }

    private void OnDisable()
    {
        UIDialogueManager.OnDialogueFinished -= ResetUIDialogue;
        UIDialogueManager.OnDialogueStarted -= SetUIDialogue;
        UIDialogueManager.OnDialogueHolding -= HoldingUIDialogue;

        if (GameManager.Instance) GameManager.Instance.Pause -= OnPause;
    }

    //example and debug purposes
    public void Button()
    {
        if (!_dialogueBalloon || !_characterPortrait) return;

        _dialogueBalloon.SetActive(true);
        _characterPortrait.SetActive(true);

        UIDialogueManager.Instance.StartDialogue(EventDialogue);
    }

    private void SetUIDialogue()
    {
        _dialogueBalloon.SetActive(true);
        _characterPortrait.SetActive(true);
    }

    private void HoldingUIDialogue()
    {
        _dialogueBalloon.SetActive(false);
        _characterPortrait.SetActive(true);
    }

    private void ResetUIDialogue()
    {
        _dialogueBalloon.SetActive(false);
        _characterPortrait.SetActive(false);
    }

    private void OnPause(bool value)
    {
        _pauseMenu.SetActive(value);

        if (!value)
        {
            if (_menuInGame) _menuInGame.SetActive(true);
            if (_menuOptions) _menuOptions.SetActive(false);
            if (_menuOptionAudio) _menuOptionAudio.SetActive(false);
            if (_menuOptionGraphics) _menuOptionGraphics.SetActive(false);
        }
    }
}
