using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject; // 座標を反映させる対象のオブジェクト

    private void Update()
    {
        // ターゲットオブジェクトのY座標を現在のオブジェクトに反映
        Vector3 newPosition = transform.position;
        newPosition.y = targetObject.position.y;
        transform.position = newPosition;
    }
}
