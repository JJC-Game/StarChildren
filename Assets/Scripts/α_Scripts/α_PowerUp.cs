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
    public Image imageToShow; // ゲージがMAXになったときに表示させるImage

    private float currentAmount = 0.0f; // 現在のゲージの値
    private bool isMaxed = false; // ゲージがMAXに達したかどうか

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (!isMaxed)
        {
            // ゲージの値を増加させる
            currentAmount += increaseAmount;

            // ゲージの値を0から最大値の範囲にクランプする
            currentAmount = Mathf.Clamp(currentAmount, 0.0f, maxAmount);

            // ゲージの表示を更新する
            gaugeImage.fillAmount = currentAmount / maxAmount;

            // ゲージがMAXに達したかどうかをチェック
            if (currentAmount >= maxAmount)
            {
                // ゲージがMAXになったらリセットする
                ResetGauge();

                isMaxed = true;
                // ゲージがMAXになったら新しいオブジェクトを表示する
                ShowImage();
            }
        }
    }

    void ResetGauge()
    {
        // ゲージの値をリセットする
        currentAmount = 0.0f;

        // ゲージの表示を更新する
        gaugeImage.fillAmount = currentAmount / maxAmount;
    }



void ShowImage()
    {
        // オブジェクトをアクティブにする
        imageToShow.gameObject.SetActive(true);
    }
}