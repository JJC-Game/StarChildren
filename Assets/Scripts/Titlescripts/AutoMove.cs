using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoMove : MonoBehaviour
{
    public float moveSpeed; // 移動速度
    public float moveDuration; // 移動する時間（秒）
    public float shakeIntensity; // 揺れの強度
    public float shakeDuration; // 揺れる時間（秒）

    public List<Vector3> movePoints; // 移動ポイントのリスト
    private int currentMovePointIndex = 0; // 現在の移動ポイントのインデックス
    private Rigidbody2D rb; // Rigidbody2D コンポーネント
    private bool isMoving = true; // 移動中かどうかのフラグ

    private Vector3 originalPosition; // オリジナルの位置
    private Coroutine shakeCoroutine; // 揺れ動きのコルーチン

    private void Start()
    {
        // Rigidbody2D コンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        if (movePoints.Count == 0)
        {
            Debug.LogWarning("移動ポイントが設定されていません。");
            return;
        }

        // オリジナルの位置を保存
        originalPosition = transform.position;

        // 最初の移動ポイントに向かって移動を開始
        MoveToNextPoint();
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0)) // 左クリックが押されたら
            {
                // 再び移動を開始
                MoveToNextPoint();
                Debug.Log("再び移動");
            }
            else if (Input.GetKeyDown(KeyCode.F1))
            {
                // F1キーが押されたら現在のシーンを再読み込み
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("シーンリセット");
            }
        }
    }

    private void MoveToNextPoint()
    {
        // 現在の移動ポイントを取得
        Vector3 targetPoint = movePoints[currentMovePointIndex];

        // 移動の方向を計算
        Vector3 moveDirection = (targetPoint - transform.position).normalized;

        // 移動開始
        rb.velocity = moveDirection * moveSpeed;
        isMoving = true;

        // 指定した秒数後に停止するコルーチンを開始
        StartCoroutine(StopMovingAfterDuration());

        // 最後の移動ポイントに到達したら移動を停止
        if (currentMovePointIndex < movePoints.Count - 1)
        {
            currentMovePointIndex++;
        }
        else
        {
            isMoving = false;
            Debug.Log("すべての移動ポイントに到達しました。");
        }
    }

    private IEnumerator StopMovingAfterDuration()
    {
        yield return new WaitForSeconds(moveDuration);

        // 移動を停止
        rb.velocity = Vector2.zero;
        isMoving = false;
        Debug.Log("停止したよ");

        // 揺れ動きを開始
        if (shakeCoroutine == null)
        {
            shakeCoroutine = StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // ランダムな揺れ動きを生成
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            Vector3 newPosition = originalPosition + randomOffset;

            // オブジェクトの位置を更新
            //transform.position = newPosition;

            // 時間を経過
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 揺れ動きが終了したらオブジェクトの位置を元に戻す
        //transform.position = originalPosition;

        // 揺れ動きのコルーチンをリセット
        shakeCoroutine = null;
    }
}
