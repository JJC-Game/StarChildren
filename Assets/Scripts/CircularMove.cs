using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMove : MonoBehaviour
{
    public float radius = 2.0f;          // 円の半径
    public float speed = 1.0f;          // 移動速度

    private Vector2 centerPosition;     // 円の中心座標
    private float angle = 0.0f;         // 角度

    public bool isRotating;

    private void Start()
    {
        isRotating = false;
    }

    void Update()
    {
        if (isRotating)
        {
        // 角度を増加させ、新しい位置を計算
        angle += speed * Time.deltaTime;

        // 新しい位置を計算し、オブジェクトを移動させる
        float x = centerPosition.x + radius * Mathf.Cos(angle);
        float y = centerPosition.y + radius * Mathf.Sin(angle);

        transform.position = new Vector2(x, y);
            DataManager.Instance.SaveBool("Home", true);

        }
        
    }

    public void ChangeRotation()
    {
        isRotating = true;
        centerPosition = transform.position;
    }
}
