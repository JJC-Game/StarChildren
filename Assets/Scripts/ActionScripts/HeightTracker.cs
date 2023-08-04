using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeightTracker : MonoBehaviour
{
    public TextMeshProUGUI heightText; // 高さを表示するテキスト

    private void Update()
    {
        // プレイヤーの現在の高さを取得
        float playerHeight = transform.position.y+7;

        // 高さをテキストに表示
        heightText.text = "Height:" + Mathf.RoundToInt(playerHeight).ToString()  + "m";
    }
}

