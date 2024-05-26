using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class TutorialManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<UIEventDialogue> tutorialEvents = new();

    private int index = 0;
    public static int taskIndex = 0;

    public static bool TutorialIsRunning = true;

    public static bool[] TaskIsRunning = new bool[12] { true, true, true, true, true, true, true, true, true, true, true, true };

    private bool isTutorialSkipped = false;

    public delegate void Event();
    public static event Event Finished = null;

    private void Start()
    {
        NextTutorial();
    }

    private void OnEnable()
    {
        CaldronManager.TutorialTask1 += NextTask;
    }

    private void OnDisable()
    {
        CaldronManager.TutorialTask1 -= NextTask;
    }

    private void NextTutorial()
    {
        if (index < tutorialEvents.Count)
        {
            DialogueManager.Instance.ShowEvent(DialogueManager.STATE.TUTORIAL, tutorialEvents[index]);
            index++;
        }
        else if (!isTutorialSkipped)
        {
            SkipTutorial();
        }
    }

    private void NextTask(int taskId)
    {
        if (taskId > taskIndex && taskId < taskIndex + 2)
        {
            NextTutorial();
            taskIndex++;
        }
    }

    /// <summary>
    /// Skips the tutorial
    /// </summary>
    public void SkipTutorial()
    {
        isTutorialSkipped = true;
        index = tutorialEvents.Count;
        Finished?.Invoke();

        TutorialIsRunning = false;

        for (int i = 0; i < TaskIsRunning.Length; i++)
        {
            TaskIsRunning[i] = false;
        }
    }
}
