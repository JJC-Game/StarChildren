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
    private Vector2 gravity;
    private float angularVelocity;
    public bool isOnFloor;
    public bool Beta;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale * Physics2D.gravity;
        angularVelocity = rb.angularVelocity;
    }

    private void Update()
    {
        if (isOnFloor && GameManager.Instance.mainGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swipeStartPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swipeEndPosition = Input.mousePosition;
                DetectSwipeDirection();

            }
        }
    }

    private void DetectSwipeDirection()
    {
        Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
        float swipeDistance = swipeDirection.magnitude;

        if (swipeDistance >= swipeDistanceThreshold && isOnFloor && GameManager.Instance.mainGame)
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
            DataManager.Instance.SaveInt("BetaPoint", 0);
        }

        // 壁に触れたらくっつく、BetaPointがない場合はくっつかない
        if (collision.gameObject.CompareTag("Wall") && DataManager.Instance.LoadInt("BetaPoint") >= 1)
        {
            isOnFloor = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            DataManager.Instance.SaveInt("BetaPoint", 0);
            Beta = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 床から離れた場合、isOnFloorをfalseにする
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }

        // 一度壁にくっついた後壁から離れるとBetaCountを減らし、重力と回転を戻す
        if (collision.gameObject.CompareTag("Wall") && Beta == true)
        {
            rb.angularVelocity = angularVelocity;
            rb.gravityScale = 0.8f;
            isOnFloor = false;
            Beta = false;
        }
    }
}
