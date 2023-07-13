using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class α_PowerUp : MonoBehaviour
{
    public List<Button> buttons; //押せるボタンのリスト
    public Image gaugeImage;
    public float increaseAmount = 0.1f; // ゲージの増加量
    public float maxAmount = 1.0f; // ゲージの最大値
    public Image imageToChange; // ゲージがMAXになったときに表示させるImage
    public Sprite newImageSprite; // ゲージがMAXになったときに表示する新しいImageのSprite

    private float currentAmount = 0.0f; // 現在のゲージの値
    private bool isMaxed = false; // ゲージがMAXに達したかどうか
    private bool isResetting = false; // ゲージがリセット中かどうか

    private void Start()
    {
        // 各ボタンに対してイベントリスナーを追加
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (isResetting)
            return;
        if (buttons.Contains(clickedButton))
        {
            int index = buttons.IndexOf(clickedButton);
            Debug.Log("ボタン" + (index + 1) + "が押されました。進行処理" + (index + 1) + "を実行します。");

            // ゲージの値を増加させる
            currentAmount += increaseAmount;

        // ゲージの値を0から最大値の範囲にクランプする
        currentAmount = Mathf.Clamp(currentAmount, 0.0f, maxAmount);

        // ゲージの表示を更新する
        gaugeImage.fillAmount = currentAmount / maxAmount;

        // もしゲージがMAXに達したら
            if (currentAmount >= maxAmount)
            {
            // ゲージがMAXになったらリセットする
            ResetGauge();

            isMaxed = true;
            // ゲージがMAXになったら新しいオブジェクトを表示する
            ChangeImage();
            }
        }
    }

    void ResetGauge()
    {
        // ゲージの値をリセットする
        currentAmount = 0.0f;

        // ゲージの表示を更新する
        gaugeImage.fillAmount = currentAmount / maxAmount;

        // ゲージがリセット中のフラグを立てる
        isResetting = true;

        // リセット後の待機時間を設定し、その後にリセットフラグを解除する
        float resetDelay = 2.0f; // リセット後の待機時間（秒）
        Invoke("ResetComplete", resetDelay);
    }

    void ResetComplete()
    {
        // リセットフラグを解除する
        isResetting = false;
    }



    void ChangeImage()
    {
        // ImageのSpriteを新しいSpriteに変更する
        imageToChange.gameObject.SetActive(true);
    }
}