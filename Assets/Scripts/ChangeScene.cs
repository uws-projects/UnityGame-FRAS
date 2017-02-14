using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void StartRace()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
