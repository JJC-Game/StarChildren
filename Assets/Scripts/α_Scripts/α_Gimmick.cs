using UnityEngine;

public class α_Gimmick : MonoBehaviour
{
    private bool isPlayerOnFloor = false;
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
    }
}
