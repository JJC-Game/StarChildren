using UnityEngine;

public class SwipeObject : SwipeController2D
{
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool isSwiping = false;

    // オブジェクトの最小・最大大きさ
    public float minSize = 0.5f;
    public float maxSize = 2f;

    // 画像を変えるためのオブジェクト
    public GameObject imageObject;

    private Quaternion initialRotation;
    private Vector3 initialScale;

    private void Start()
    {
        // 初期の回転と大きさを保存
        initialRotation = imageObject.transform.rotation;
        initialScale = imageObject.transform.localScale;
    }

    private void Update()
    {
        // メインゲームがtrueかつタップされている間画像を表示
        if (Input.GetMouseButtonDown(0) && ObjectView && GameManager.Instance.mainGame)
        {
            startPosition = Input.mousePosition;
            isSwiping = true;
            // オブジェクトを表示する
            imageObject.SetActive(true);
        }
        // 二段ジャンプの能力がない状態で空中にいるときに画像を表示しない
        else if (DataManager.Instance.LoadInt("PataLimit") == 0 && Pata == true || PauseManager.Instance.isPause)
        {
            isSwiping = false;
            imageObject.SetActive(false);
        }
        // メインゲームがtrueかつタップしていない間画像を表示しない
        else if (Input.GetMouseButtonUp(0) && GameManager.Instance.mainGame)
        {
            endPosition = Input.mousePosition;
            isSwiping = false;
            imageObject.SetActive(false);
        }
        
        // スワイプしている間は画像の大きさ、角度を変える
        if (isSwiping)
        {
            endPosition = Input.mousePosition;
            Vector2 swipeVector = endPosition - startPosition;

            // スワイプの方向に応じてオブジェクトの方向を変える
            float angle = Mathf.Atan2(swipeVector.y, swipeVector.x) * Mathf.Rad2Deg;
            imageObject.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

            // スワイプの大きさに応じてオブジェクトの大きさを変える
            float swipeMagnitude = swipeVector.magnitude;
            float sizePercentage = swipeMagnitude / Screen.width;
            float newSize = Mathf.Lerp(minSize, maxSize, sizePercentage);
            imageObject.transform.localScale = new Vector3(1f, newSize, 1f);
        }
        else
        {
            // オブジェクトが非表示の場合は、回転と大きさを初期値に戻す
            imageObject.transform.rotation = initialRotation;
            imageObject.transform.localScale = initialScale;
        }
    }
}
