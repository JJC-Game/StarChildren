using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
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
            MainGame.enabled = true;
            PauseCanvas.enabled = false;
            isPause = false;
            Time.timeScale = 1; // ゲーム再開

        }
    }

    public void ResetPause()
    {
        MainGame.enabled = true;
        PauseCanvas.enabled = false;
        isPause = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1; 
    }

    public void HomePause()
    {
        MainGame.enabled = true;
        PauseCanvas.enabled = false;
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

}
