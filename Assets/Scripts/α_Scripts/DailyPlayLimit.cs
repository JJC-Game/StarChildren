using UnityEngine;

public class DailyPlayLimit : MonoBehaviour
{
    private const string PlayCountKey = "PlayCount";
    private const string LastClearDateKey = "LastClearDate";

    private const int MaxPlayCount = 1; // 一日の最大プレイ回数

    private int playCount;
    private string lastClearDate;

    private void Start()
    {
        // プレイ回数と最終クリア日を読み込む
        playCount = PlayerPrefs.GetInt(PlayCountKey, 0);
        lastClearDate = PlayerPrefs.GetString(LastClearDateKey, "");
    }

    private void OnApplicationQuit()
    {
        // プレイ回数と最終クリア日を保存する
        PlayerPrefs.SetInt(PlayCountKey, playCount);
        PlayerPrefs.SetString(LastClearDateKey, lastClearDate);
    }

    public void PlayGame()
    {
        if (lastClearDate != GetCurrentDate() || playCount < MaxPlayCount)
        {
            // クリアしていない日またはプレイ可能な場合はゲームを開始
            // ここにゲームの開始処理を追加
            Debug.Log("Game started!");

            if (lastClearDate != GetCurrentDate())
            {
                // 新しい日になった場合はプレイ回数をリセットする
                playCount = 0;
            }

            playCount++;
        }
        else
        {
            // プレイ回数制限に達した場合の処理
            Debug.Log("You have reached the daily play limit.");
            // ここにプレイ回数制限に達した場合の処理を追加
        }
    }

    public void ClearGame()
    {
        // ゲームをクリアした場合の処理
        Debug.Log("Game cleared!");

        lastClearDate = GetCurrentDate();
    }

    private string GetCurrentDate()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd");
    }
}
