using UnityEngine;
using UnityEngine.UI;

public class HeightTracker : MonoBehaviour
{
    public Text heightText; // 高さを表示するテキスト

    private void Update()
    {
        // プレイヤーの現在の高さを取得
        float playerHeight = transform.position.y;

        // 高さをテキストに表示
        heightText.text = "Height: " + playerHeight.ToString("F1") + "m";
    }
}
