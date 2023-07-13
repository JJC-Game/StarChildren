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
        if (Input.GetMouseButtonDown(0) && isOnFloor == true && GameManager.Instance.mainGame)
        {
            startPosition = Input.mousePosition;
            isSwiping = true;
            // オブジェクトを表示する
            imageObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0) )
        {
            endPosition = Input.mousePosition;
            isSwiping = false;
            imageObject.SetActive(false);
        }

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
