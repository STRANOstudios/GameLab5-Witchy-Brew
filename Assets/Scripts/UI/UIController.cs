using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _pauseMenu;

    [Header("Menu Elements")]
    [SerializeField] private GameObject _menuInGame;
    [SerializeField] private GameObject _menuOptions;
    [SerializeField] private GameObject _menuOptionAudio;
    [SerializeField] private GameObject _menuOptionGraphics;

    private void OnEnable()
    {
        if (GameManager.Instance) GameManager.Instance.Pause += OnPause;
    }

    private void OnDisable()
    {
        if (GameManager.Instance) GameManager.Instance.Pause -= OnPause;
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
