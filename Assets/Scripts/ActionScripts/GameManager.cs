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
    public GameObject StartTimeLineCanvas;

    public GameObject PlayerSprite; //キャラクターの見た目
    public Sprite[] EvolveSprite; //進化後のキャラクターの見た目

    public TextMeshProUGUI timerText; // 残り時間を表示するテキスト
    public TextMeshProUGUI heightText; // 高さを表示するテキスト
    public TextMeshProUGUI BestheightText; // 最高記録を表示するテキスト
    public string high;
    public string besthigh;

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

        if (DataManager.Instance.LoadBool("E1F") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[0];
        }
        else if (DataManager.Instance.LoadBool("E1O") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[1];
        }
        else if (DataManager.Instance.LoadBool("E2FF") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[2];
        }
        else if (DataManager.Instance.LoadBool("E2OO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[3];
        }
        else if (DataManager.Instance.LoadBool("E2FO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[4];
        }
        else if (DataManager.Instance.LoadBool("E3FFF") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[5];
        }
        else if (DataManager.Instance.LoadBool("E3OOO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[6];
        }
        else if (DataManager.Instance.LoadBool("E3FFO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[7];
        }
        else if (DataManager.Instance.LoadBool("E3FOO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[8];
        }
        else if (DataManager.Instance.LoadBool("E1") == false)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[9];
        }

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

        if (StartTimeLine.state == PlayState.Playing && Input.GetMouseButtonDown(0))
        {
            DemoSkip();
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
        DataManager.Instance.SaveFloat("PlayerHigh",playerHeight);

        if (playerHeight > DataManager.Instance.LoadFloat("PlayerBest1"))
        {
            if (DataManager.Instance.LoadInt("Stage") == 1)
            {
                DataManager.Instance.SaveFloat("PlayerBest1", playerHeight);
            }
            else if (DataManager.Instance.LoadInt("Stage") == 2)
            {
                DataManager.Instance.SaveFloat("PlayerBest2", playerHeight);
            }
            else if (DataManager.Instance.LoadInt("Stage") == 3)
            {
                DataManager.Instance.SaveFloat("PlayerBest3", playerHeight);
            }

        }

        // 高さをテキストに表示
        heightText.text = Mathf.RoundToInt(DataManager.Instance.LoadFloat("PlayerHigh")).ToString();
        if(DataManager.Instance.LoadInt("Stage") == 1)
        {
            BestheightText.text = Mathf.RoundToInt(DataManager.Instance.LoadFloat("PlayerBest1")).ToString();
        }
        else if (DataManager.Instance.LoadInt("Stage") == 2)
        {
            BestheightText.text = Mathf.RoundToInt(DataManager.Instance.LoadFloat("PlayerBest2")).ToString();
        }
        else if (DataManager.Instance.LoadInt("Stage") == 3)
        {
            BestheightText.text = Mathf.RoundToInt(DataManager.Instance.LoadFloat("PlayerBest3")).ToString();
        }
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

    public void DemoSkip()
    {
        mainGame = true;
        StartTimer();
        StartTimeLine.Stop();

        MainGameCanvas.SetActive(true);
        StartTimeLineCanvas.SetActive(false);
    }

}
