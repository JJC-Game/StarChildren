using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right; // 移動する方向
    public float moveSpeed; // 移動速度
    public float moveDuration; // 移動する時間（秒）

    private Rigidbody2D rb; // Rigidbody2D コンポーネント
    private bool isMoving = true; // 移動中かどうかのフラグ

    private void Start()
    {
        // Rigidbody2D コンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        // シーンの開始時に自動的に移動を開始する
        rb.velocity = moveDirection.normalized * moveSpeed;
        Debug.Log("移動開始したよ");

        // 指定した秒数後に停止するコルーチンを開始
        StartCoroutine(StopMovingAfterDuration());
    }

    private IEnumerator StopMovingAfterDuration()
    {
        yield return new WaitForSeconds(moveDuration);

        // 移動を停止
        rb.velocity = Vector2.zero;
        isMoving = false;
        Debug.Log("停止したよ");
    }
}