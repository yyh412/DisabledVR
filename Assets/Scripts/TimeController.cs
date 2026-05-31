using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float StartTime = 180f;

    private float TimeLeft;
    private bool timerRunning = true;

    public TMP_Text TimerText;

    void Start()
    {
        TimeLeft = StartTime;

        if (TimerText != null)
        {
            TimerText.gameObject.SetActive(true);
            FormatToMinSec();
        }
    }

    void Update()
    {
        if (!timerRunning) return;

        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            FormatToMinSec();
        }
        else
        {
            TimeLeft = 0;
            timerRunning = false;
            FormatToMinSec();

            Debug.Log("Interview Time Up");
        }
    }

    void FormatToMinSec()
    {
        int mins = Mathf.FloorToInt(TimeLeft / 60);
        int secs = Mathf.FloorToInt(TimeLeft % 60);

        if (TimerText != null)
            TimerText.text = string.Format("{0:00}:{1:00}", mins, secs);
    }
}