using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerStartGame : MonoBehaviour
{
    public string sceneName = "City";

    [Header("Press trigger or button")]
    public InputActionProperty startAction;

    private bool started = false;

    void Update()
    {
        if (started) return;

        if (startAction.action != null && startAction.action.WasPressedThisFrame())
        {
            started = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}