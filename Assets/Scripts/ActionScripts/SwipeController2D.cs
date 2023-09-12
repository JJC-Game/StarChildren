using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController2D : Singleton<SwipeController2D>
{
    // スワイプの感知距離
    public float swipeDistanceThreshold = 50f;

    // オブジェクトの移動速度
    public float speed = 5f;

    // スワイプに関する変数
    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;

    // べたべたに関する変数
    private Rigidbody2D rb;
    //private Vector2 gravity;
    //private float angularVelocity;
    //private float timer = 0f;
    //private bool timeron;
    //private int BetaCount = 1;

    public bool isOnFloor; // 地面についてるかどうか
    //public bool Pata;  // ぱたぱたの能力を持っているか
    //public bool Beta; // 壁にくっついているか
    //public bool MoreJump; // 二段ジャンプできる状態か
    public bool ObjectView; // 矢印の画像が表示されるか

    public Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //gravity = rb.gravityScale * Physics2D.gravity;
        //angularVelocity = rb.angularVelocity;
        //DataManager.Instance.SaveInt("PataLimit", DataManager.Instance.LoadInt("PataCount"));
        /*
        if (DataManager.Instance.LoadInt("PataCount") >= 1)
        {
            Pata = true;
        }
        else
        {
            Pata = false;
        }
        */

    }

    private void Update()
    {
        // スワイプ操作の処理
        if (isOnFloor && GameManager.Instance.mainGame)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                swipeStartPosition = Input.mousePosition;
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
            {
                swipeEndPosition = Input.mousePosition;
                DetectSwipeDirection();
                EffectManager.Instance.PlayEffect(1, player);
                SoundManager.Instance.PlaySE_Sys(5);


            }
           
        }

        /*
        // ぱたぱたの能力を持っていて空中にいる状態で指定の回数ジャンプできる処理
        if (isOnFloor == false && GameManager.Instance.mainGame && DataManager.Instance.LoadInt("PataLimit") >= 1 && MoreJump)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0) && MoreJump)
            {
                swipeStartPosition = Input.mousePosition;
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
            {
                swipeEndPosition = Input.mousePosition;
                DetectSwipeDirection();
                ObjectView = false;
                DataManager.Instance.SaveInt("PataLimit", DataManager.Instance.LoadInt("PataLimit") - 1);

            }
        }
        */

        /*
        // 壁にくっついてるときにタイマーの計測を開始する処理
        if (timeron)
        {
            timer += Time.deltaTime;
        }

        // タイマーが既定の時間になったら壁から離れて落ちる処理
        if (Beta && timer >= DataManager.Instance.LoadFloat("BetaMin"))
        {
            rb.angularVelocity = angularVelocity;
            rb.gravityScale = 0.8f;
            isOnFloor = false;
            Beta = false;
            timer = 0f;
        }
        */
    }

    // スワイプの距離計測と飛ばす処理
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
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Omob"))
        {
            ObjectView = true;
            isOnFloor = true;
            //MoreJump = false;
            //DataManager.Instance.SaveInt("PataLimit", DataManager.Instance.LoadInt("PataCount"));
            //BetaCount = 1;
        }

        /*
        // 壁に触れたらくっつく、BetaCountがない場合はくっつかない
        if (collision.gameObject.CompareTag("Wall") && DataManager.Instance.LoadBool("Beta") && BetaCount >=1)
        {
            ObjectView = true;
            isOnFloor = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            //Beta = true;
            timer = 0f;
            timeron = true;
            BetaCount = -1;
        }
        */

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Omob"))
        {
            ObjectView = true;
            isOnFloor = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 床から離れた場合、isOnFloorをfalseにする
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Omob"))
        {
            isOnFloor = false;

            /*
            if (DataManager.Instance.LoadInt("PataLimit") >= 1)
            {
                //MoreJump = true;
                ObjectView = true;
            }
            else
            {
                ObjectView = false;
            }
            */
        }

        /*
        // 一度壁にくっついた後壁から離れる重力と回転を戻す
        if (collision.gameObject.CompareTag("Wall") && Beta == true)
        {
            rb.angularVelocity = angularVelocity;
            rb.gravityScale = 0.8f;
            isOnFloor = false;
            Beta = false;
            ObjectView = false;
        }
        */
    }

}
