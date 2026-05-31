using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string sceneName = "City";

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}