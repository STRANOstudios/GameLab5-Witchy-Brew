using UnityEngine;

public class Task1 : MonoBehaviour
{
    public delegate void Event(int id);
    public static event Event TutorialTask0 = null;

    void Update()
    {
        if (TutorialManager.TutorialIsRunning)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (TutorialManager.TaskIsRunning[0]) return;

                if (TutorialManager.taskIndex == 0) TutorialTask0?.Invoke(1);
            }
        }
    }
}
