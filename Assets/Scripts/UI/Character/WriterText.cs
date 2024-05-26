using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent, RequireComponent(typeof(TMP_Text))]
public class WriterText : MonoBehaviour
{
    private enum STATE
    {
        Playing,
        Finished,
        None
    }

    private float timePerCharacter = 0.05f;
    private float lineDuration = 1f;

    private TMP_Text _text;

    private List<string> lines = new();
    private int index = 0;

    private STATE currentState;

    public delegate void Next();
    public static event Next NextAnim = null;
    public static event Next Finished = null;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        timePerCharacter = DialogueManager.Instance.TimePerCharacter;
        lineDuration = DialogueManager.Instance.LineDuration;
    }

    /// <summary>
    /// Starts the dialogue
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="delay"></param>
    public void Write(List<string> lines, bool delay = true)
    {
        index = 0;
        this.lines = lines;
        StartCoroutine(WriteText(lines, delay));
    }

    private IEnumerator WriteText(List<string> lines, bool delay)
    {
        currentState = STATE.Playing;

        NextAnim?.Invoke();

        for (int i = index; i < lines.Count; i++)
        {
            _text.text = "";

            foreach (char c in lines[i])
            {
                _text.text += c;
                yield return new WaitForSeconds(timePerCharacter);
            }

            currentState = STATE.Finished;

            if (delay && lines.Count > 1) yield return new WaitForSeconds(lineDuration);

            index++;
        }

        End();
    }

    /// <summary>
    /// Called when the button is pressed
    /// Finnishes the current dialogue or starts the next
    /// </summary>
    public void ButtonPressed()
    {
        if (currentState == STATE.Playing)
        {
            StopAllCoroutines();
            _text.text = lines[index];  // Complete the current dialogue instantly
            currentState = STATE.Finished;
        }
        else if (currentState != STATE.None)
        {
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        if (currentState == STATE.Finished)
        {
            if (index < lines.Count - 1)
            {
                index++;
                StartCoroutine(WriteText(lines, true));
                return;
            }

            End();
        }
    }

    private void End()
    {
        currentState = STATE.None;
        index = 0;
        Finished?.Invoke();

        if (TutorialManager.TutorialIsRunning)
        {
            TutorialManager.TaskIsRunning1 = false;
        }
    }
}
