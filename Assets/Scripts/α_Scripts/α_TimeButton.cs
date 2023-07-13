using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class α_TimeButton : MonoBehaviour
{
    public Button actionButton;
    private DateTime lastActionTime;
    private TimeSpan cooldownDuration = TimeSpan.FromMinutes(1);

    private void Start()
    {
        lastActionTime = DateTime.MinValue;
        actionButton.onClick.AddListener(OnActionButtonClick);
    }

    private void OnActionButtonClick()
    {
        if (CanPerformAction())
        {
            // ゲームシーンに遷移する処理を記述します
            SceneManager.LoadScene("α_ijiri");
            // または、別の方法でゲームシーンに遷移する処理を追加してください
        }
        if (CanPerformAction())
        {
            // Actionを実行する処理を記述します
            lastActionTime = DateTime.Now;
            // ここにActionの実行処理を追加してください
        }
    }

    private bool CanPerformAction()
    {
        // 前回のActionから一分以上経過しているかチェックします
        TimeSpan timeSinceLastAction = DateTime.Now - lastActionTime;
        if (timeSinceLastAction >= cooldownDuration)
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
        // ボタンの有効/無効と色を設定します
        actionButton.interactable = CanPerformAction();
        if (actionButton.interactable)
        {
            // ボタンが押せる場合の色を設定します
            actionButton.image.color = Color.white;
        }
        else
        {
            // ボタンが押せない場合の色を設定します
            actionButton.image.color = Color.gray;
        }
    }
}