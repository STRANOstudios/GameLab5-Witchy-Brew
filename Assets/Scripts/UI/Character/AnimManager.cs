using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(Animator))]
public class AnimManager : MonoBehaviour
{
    private Animator _animator;

    private List<string> _animationNames = new(); // The names of AnimationClips
    private int index = 0;

    private float fadeDelay = 0.5f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        fadeDelay = DialogueManager.Instance.FadeDelay;
    }

    /// <summary>
    /// Inizialization of the AnimationManager
    /// </summary>
    /// <param name="animationNames"></param>
    public void Play(List<string> animationNames)
    {
        index = 0;
        _animationNames.Clear();
        _animationNames = animationNames;
        NextAnimation();
    }

    private void OnEnable()
    {
        WriterText.NextAnim += NextAnimation;
    }

    private void OnDisable()
    {
        WriterText.NextAnim -= NextAnimation;
    }

    private void NextAnimation()
    {
        if (index < _animationNames.Count)
        {
            _animator.CrossFade(_animationNames[index], fadeDelay);
            index++;
        }
    }
}
