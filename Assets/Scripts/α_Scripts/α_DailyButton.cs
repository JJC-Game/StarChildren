using UnityEngine;
using UnityEngine.UI;
using System;

public class α_DailyButton : MonoBehaviour
{
    public Button button;
    private DateTime lastClickedTime;

    private void Start()
    {
        lastClickedTime = DateTime.MinValue;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (CanClick())
        {
            // ボタンが押せる場合の処理を記述します
            lastClickedTime = DateTime.Now;
        }
    }

    private bool CanClick()
    {
        // 現在の日付と最後にクリックした日付を比較し、一日経過しているかチェックします
        TimeSpan timeSinceLastClick = DateTime.Now - lastClickedTime;
        if (timeSinceLastClick.TotalDays >= 1)
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
        button.interactable = CanClick();
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
}
