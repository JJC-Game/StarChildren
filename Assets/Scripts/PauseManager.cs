using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public Canvas PauseCanvas;
    public Canvas MainGame;
    
    void Start()
    {
        PauseCanvas.enabled = false;
    }

    
    void Update()
    {
        
    }

    void Pause()
    {
        GameManager.Instance.mainGame = false;
        MainGame.enabled = false;
        PauseCanvas.enabled = true;
        Time.timeScale = 0f; // ゲーム時間を停止します

    }

}
