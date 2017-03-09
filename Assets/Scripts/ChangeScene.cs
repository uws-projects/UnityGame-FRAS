using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void LoadPreviousLevel()
    {
        Application.LoadLevel(Application.loadedLevel - 1);
    }

    public void SendFeedback()
    {
        Application.OpenURL("https://goo.gl/forms/x0zK5kF3OOs7Hf2C3");
    }

}