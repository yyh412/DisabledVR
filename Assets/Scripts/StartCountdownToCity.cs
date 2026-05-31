using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartCountdownToCity : MonoBehaviour
{
    public string sceneName = "City";
    public InputActionProperty startAction;
    public TMP_Text countdownText;
    public GameObject startButton;

    private bool started = false;

    void Start()
    {
        if (countdownText != null)
        {
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (started) return;

        if (startAction.action != null && startAction.action.WasPressedThisFrame())
        {
            started = true;
            StartCoroutine(CountdownRoutine());
        }
    }

    IEnumerator CountdownRoutine()
    {
        if (startButton != null)
            startButton.SetActive(false);

        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO";
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneName);
    }
}