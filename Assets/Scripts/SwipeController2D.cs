using UnityEngine;

public class SwipeController2D : MonoBehaviour
{
    // スワイプの感知距離
    public float swipeDistanceThreshold = 50f;

    // オブジェクトの移動速度
    public float speed = 5f;

    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;
    private Rigidbody2D rb;
    public bool isOnFloor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isOnFloor)
        {
            swipeStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && isOnFloor)
        {
            swipeEndPosition = Input.mousePosition;
            DetectSwipeDirection();
        }
    }

    private void DetectSwipeDirection()
    {
        Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
        float swipeDistance = swipeDirection.magnitude;

        if (swipeDistance >= swipeDistanceThreshold && isOnFloor)
        {
            swipeDirection.Normalize();

            // スワイプの大きさに応じてスピードを計算する
            float calculatedSpeed = swipeDistance / swipeDistanceThreshold * speed;

            Vector2 oppositeDirection = -swipeDirection;

            // オブジェクトを逆方向に移動させる
            rb.velocity = oppositeDirection * calculatedSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 床に接触した場合、isOnFloorをtrueにする
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 床から離れた場合、isOnFloorをfalseにする
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }
}