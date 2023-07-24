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
    private float timer = 0f;
    private bool timeron;
    public int PataCount;
    public bool isOnFloor;
    public bool Beta;
    public bool MoreJump;
    public bool ObjectView;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale * Physics2D.gravity;
        angularVelocity = rb.angularVelocity;
        DataManager.Instance.SaveInt("BetaLimit", DataManager.Instance.LoadInt("BetaCount"));
        DataManager.Instance.SaveInt("PataLimit", DataManager.Instance.LoadInt("PataCount"));
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
                isOnFloor = false;
                ObjectView = false;

            }
           
        }

        if (isOnFloor == false && GameManager.Instance.mainGame && DataManager.Instance.LoadInt("PataLimit") >= 1)
        {
            ObjectView = true;
            if (Input.GetMouseButtonDown(0) && DataManager.Instance.LoadInt("PataLimit") >= 1&& MoreJump)
            {
                swipeStartPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && DataManager.Instance.LoadInt("PataLimit") >= 1 && MoreJump)
            {
                swipeEndPosition = Input.mousePosition;
                DetectSwipeDirection();
                MoreJump = false;
                ObjectView = false;
                DataManager.Instance.SaveInt("PataLimit", DataManager.Instance.LoadInt("PataLimit") - 1);

            }
        }

            if (timeron)
        {
            timer += Time.deltaTime;
        }

        if (Beta && timer >= 1f)
        {
            rb.angularVelocity = angularVelocity;
            rb.gravityScale = 0.8f;
            isOnFloor = false;
            Beta = false;
            timer = 0f;
        }
    }

    private void DetectSwipeDirection()
    {
        Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
        float swipeDistance = swipeDirection.magnitude;

        if (swipeDistance >= swipeDistanceThreshold&& GameManager.Instance.mainGame)
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
            ObjectView = true;
            isOnFloor = true;
            DataManager.Instance.SaveInt("BetaLimit", DataManager.Instance.LoadInt("BetaCount"));
            //DataManager.Instance.SaveInt("PataLimit", DataManager.Instance.LoadInt("PataCount"));
        }

        // 壁に触れたらくっつく、BetaCountがない場合はくっつかない
        if (collision.gameObject.CompareTag("Wall") && DataManager.Instance.LoadInt("BetaLimit") >= 1)
        {
            ObjectView = true;
            isOnFloor = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            Beta = true;
            timer = 0f;
            timeron = true;
            DataManager.Instance.SaveInt("BetaLimit", DataManager.Instance.LoadInt("BetaLimit") - 1);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 床から離れた場合、isOnFloorをfalseにする
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
            MoreJump = true;
            ObjectView = false;
        }

        // 一度壁にくっついた後壁から離れるとBetaCountを減らし、重力と回転を戻す
        if (collision.gameObject.CompareTag("Wall") && Beta == true)
        {
            rb.angularVelocity = angularVelocity;
            rb.gravityScale = 0.8f;
            isOnFloor = false;
            Beta = false;
            ObjectView = false;
        }
    }
}
