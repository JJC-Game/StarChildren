using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    public bool mainGame;
    public bool GameClear;
    public bool TimeUp;
    public float targetTime = 90f; // 目標の経過時間（1分30秒）
    public Canvas TimeUpCanvas;
    //public PlayableDirector timeline; // ゲーム終了時に再生するタイムライン

    public TextMeshProUGUI timerText; // 表示するTextMeshProUGUIオブジェクト

    private float startTime;
    private float remainingTime;
    private bool isTimerRunning;
    //private bool isInputEnabled = true; // マウスクリックの入力が有効かどうか

    void Start()
    {
        TimeUpCanvas.enabled = false;
        mainGame = true;
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            remainingTime = Mathf.Max(targetTime - elapsedTime, 0f); // 残り時間を計算

            UpdateTimerText(remainingTime);

            if (elapsedTime >= targetTime)
            {
                StopTimer();
                mainGame = false;
            }
        }

        if (!mainGame && remainingTime == 0)
        {
            TimeUpCanvas.enabled = true;

            // ゲームが終了した場合の処理
            EndGame();
        }
    }

    public void StartTimer()
    {
        // タイマーを開始し、開始時刻を記録する
        isTimerRunning = true;
        startTime = Time.time;
        //isInputEnabled = true;
    }

    public void StopTimer()
    {
        // タイマーを停止する
        isTimerRunning = false;
    }

    private void UpdateTimerText(float remainingTime)
    {
        // 残り時間をテキストオブジェクトに表示する
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        int milliseconds = Mathf.FloorToInt((remainingTime * 1000) % 1000);
        string timeString = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        timerText.text = timeString;
    }

    private void EndGame()
    {
        // ゲーム終了時の処理
        // タイムラインを再生し、操作を無効化する
        /*if (timeline != null)
        {
            timeline.Play();
        }*/

        // マウスクリックの入力を無効化する
        //isInputEnabled = false;
    }
}
