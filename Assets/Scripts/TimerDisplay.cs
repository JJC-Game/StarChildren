using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText; // 表示するTextMeshProUGUIオブジェクト

    private float startTime;
    private bool isTimerRunning;

    private void Start()
    {
        // 計測を開始する
        StartTimer();
    }

    private void Update()
    {
        // タイマーが実行中の場合、経過時間を更新して表示する
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void StartTimer()
    {
        // タイマーを開始し、開始時刻を記録する
        isTimerRunning = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        // タイマーを停止する
        isTimerRunning = false;
    }

    private void UpdateTimerText(float elapsedTime)
    {
        // 経過時間をテキストオブジェクトに表示する
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        string timeString = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        timerText.text = timeString;
    }
}
