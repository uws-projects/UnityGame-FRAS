using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void StartRace()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
