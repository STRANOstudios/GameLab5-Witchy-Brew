using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _dialogueBalloon;
    [SerializeField] private GameObject _characterPortrait;

    [SerializeField] private GameObject _attemptBoard;

    [SerializeField] private UIEventDialogue EventDialogue;

    [Header("Menu Elements")]
    [SerializeField] private GameObject _menuInGame;
    [SerializeField] private GameObject _menuOptions;
    [SerializeField] private GameObject _menuOptionAudio;
    [SerializeField] private GameObject _menuOptionGraphics;

    private void OnEnable()
    {
        UIDialogueManager.OnDialogueFinished += ResetUIDialogue;
        GameManager.Instance.Pause += OnPause;
    }

    private void OnDisable()
    {
        UIDialogueManager.OnDialogueFinished -= ResetUIDialogue;
        GameManager.Instance.Pause -= OnPause;
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
