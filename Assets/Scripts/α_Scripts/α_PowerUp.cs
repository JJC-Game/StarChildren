using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class α_PowerUp : MonoBehaviour
{
    public Button myButton;
    public Image gaugeImage;
    public float increaseAmount = 0.1f; // ゲージの増加量
    public float maxAmount = 1.0f; // ゲージの最大値

    private float currentAmount = 0.0f; // 現在のゲージの値

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        // ゲージの値を増加させる
        currentAmount += increaseAmount;

        // 最大値を超えた場合は最大値にクランプする
        currentAmount = Mathf.Clamp(currentAmount, 0.0f, maxAmount);

        // ゲージの表示を更新する
        gaugeImage.fillAmount = currentAmount;
    }
}