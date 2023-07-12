using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private bool isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが範囲内に入った場合
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            GameManager.Instance.mainGame = false;
        }
    }

    private void StopGame()
    {
        // ゲームの停止処理を実行する（例えばタイムスケールの変更など）
        Time.timeScale = 0f;
    }
}
