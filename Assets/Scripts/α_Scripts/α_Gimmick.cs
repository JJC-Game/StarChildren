using UnityEngine;

public class α_Gimmick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突相手がタグが"Player"の場合
        if (collision.gameObject.CompareTag("Player"))
        {
            // ここに当たり判定を有効にする処理を追加
            // 例えば、床のCollider2Dを取得してisTriggerをfalseに設定するなど
            Collider2D floorCollider = GetComponent<Collider2D>();
            if (floorCollider != null)
            {
                floorCollider.isTrigger = false; // 当たり判定を有効にする
            }
        }
    }
}