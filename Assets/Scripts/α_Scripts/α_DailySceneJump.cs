using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class α_DailySceneJump : MonoBehaviour
{
    public Button button;
    private DateTime lastJumpTime;
    private bool canJumpScene = true;
    private string homeSceneName = "HomeScene";
    private bool gameCleared = false;
    private bool sceneJumped = false;
    private TimeSpan cooldownDuration = TimeSpan.FromMinutes(1);

    private void Start()
    {
        lastJumpTime = DateTime.MinValue;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (CanJumpScene())
        {
            // シーン遷移する場合の処理を記述します
            lastJumpTime = DateTime.Now;
            canJumpScene = false;
            sceneJumped = true;
            SceneManager.LoadScene("α_ijiri");
        }
    }

    private bool CanJumpScene()
    {
        // ゲームクリア後かつシーン遷移後に再度ボタンを押せるようにする条件を追加します
        if (gameCleared && sceneJumped && (DateTime.Now - lastJumpTime) >= cooldownDuration)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == homeSceneName)
        {
            // ホームシーンに戻った場合は再度ボタンを押せるようにします
            canJumpScene = true;
            gameCleared = false; // ゲームクリアのフラグをリセットします
            sceneJumped = false; // シーン遷移フラグをリセットします
        }

        // ボタンの有効/無効と色を設定します
        button.interactable = CanJumpScene();
        if (button.interactable)
        {
            // ボタンが押せる場合の色を設定します
            button.image.color = Color.white;
        }
        else
        {
            // ボタンが押せない場合の色を設定します
            button.image.color = Color.gray;
        }
    }

    public void SetGameCleared()
    {
        // ゲームクリア時に呼び出され、フラグを設定します
        gameCleared = true;
    }
}