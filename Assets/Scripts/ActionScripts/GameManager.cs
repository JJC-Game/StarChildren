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
    public GameObject TimeUpCanvas;
    public GameObject MainGameCanvas;
    //public PlayableDirector timeline; // ゲーム終了時に再生するタイムライン

    public TextMeshProUGUI timerText; // 残り時間を表示するテキスト
    public TextMeshProUGUI heightText; // 高さを表示するテキスト
    public TextMeshProUGUI BestheightText; // 最高記録を表示するテキスト

    public GameObject Player; // 高さを取得するプレイヤー

    private float startTime;
    private float remainingTime;
    private bool isTimerRunning;

    public PlayableDirector StartTimeLine;
    public PlayableDirector TimeUpTimeLine;

    void Start()
    {
        mainGame = false;
        TimeUpCanvas.SetActive(false);
        PlayStartTimeLine();

        DataManager.Instance.ResetMukiCount();
        DataManager.Instance.ResetOmoCount();
        DataManager.Instance.ResetBetaCount();
        DataManager.Instance.ResetPataCount();
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
                High();
                PlayTimeUpTimeLine();

            }
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

    private void High()
    {
        // プレイヤーの現在の高さを取得
        float playerHeight = Player.transform.localPosition.y + 7;
        DataManager.Instance.SaveFloat("PlayerHigh1",playerHeight);

        if (playerHeight > DataManager.Instance.LoadFloat("PlayerBest1"))
        {
            DataManager.Instance.SaveFloat("PlayerBest1", playerHeight);
        }

        // 高さをテキストに表示
        heightText.text = Mathf.RoundToInt(DataManager.Instance.LoadFloat("PlayerHigh1")).ToString();
        BestheightText.text = Mathf.RoundToInt(DataManager.Instance.LoadFloat("PlayerBest1")).ToString();
    }

    private void EndGame()
    {
        // ゲーム終了時の処理
        // タイムラインを再生し、操作を無効化する
        /*if (timeline != null)
        {
            timeline.Play();
        }*/

    }

    public void MainGame()
    {
        mainGame = true;
        StartTimer();
        
    }

    public void PlayStartTimeLine()
    {
        StartTimeLine.Play();

    }

    public void PlayTimeUpTimeLine()
    {
        mainGame = false;
        TimeUpCanvas.SetActive(true);
        TimeUpTimeLine.Play();
    }

}
