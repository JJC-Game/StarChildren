using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : Singleton<PauseManager>
{
    public Canvas PauseCanvas;
    public Canvas MainGame;
    public bool isPause;
    
    void Start()
    {
        PauseCanvas.enabled = false;
        isPause = false;
    }

    public void Pause()
    {
        if (isPause == false)
        {
            GameManager.Instance.mainGame = false;
            MainGame.enabled = false;
            PauseCanvas.enabled = true;
            isPause = true;
            Time.timeScale = 0f; // ゲーム時間を停止

        }

    }

    public void Restart()
    {
        if(isPause)
        {
            GameManager.Instance.mainGame = true;
            Time.timeScale = 1; // ゲーム再開
            MainGame.enabled = true;
            PauseCanvas.enabled = false;
            isPause = false;

            SoundManager.Instance.PlaySE_Sys(6);

        }

    }

    public void ResetPause()
    {
        DataManager.Instance.ResetMukiCount();
        DataManager.Instance.ResetOmoCount();
        DataManager.Instance.ResetBetaCount();
        DataManager.Instance.ResetPataCount();
        MainGame.enabled = true;
        PauseCanvas.enabled = false;
        isPause = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;

        SoundManager.Instance.PlaySE_Sys(6);
    }

    public void HomePause()
    {
        DataManager.Instance.ResetMukiCount();
        DataManager.Instance.ResetOmoCount();
        DataManager.Instance.ResetBetaCount();
        DataManager.Instance.ResetPataCount();
        MainGame.enabled = true;
        PauseCanvas.enabled = false;
        isPause = false;
        Time.timeScale = 1;

        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MHomeScene", 1);
        //SceneManager.LoadScene(0);

    }

}
