using UnityEngine;

public class α_Gimmick : MonoBehaviour
{
    public bool activated = false;

    public void ActivateFloor()
    {
        activated = true;
        // ここで床の見た目や物理的な性質を変更できます
        // 例: 床の色を変更する、Colliderを無効にするなど
    }

    /*private bool isPlayerOnFloor = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // 最初は動かないように設定
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnFloor = true;
        }
    }

    private void Update()
    {
        if (isPlayerOnFloor)
        {
            rb.isKinematic = false; // プレイヤーが床に触れたら動き始める
            isPlayerOnFloor = false; // 二重のトリガーを防ぐためにリセット
        }
    }*/
}
