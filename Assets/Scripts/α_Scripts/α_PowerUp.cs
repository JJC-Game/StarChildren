﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class α_PowerUp : MonoBehaviour
{
    public Button DebugMukiButton;//MukiButtonを2秒後にリセットする変数の宣言

    public float[] increaseAmounts; // 一回に増える増加量
    public float[] maxAmounts; // 最大量
    public Image[] gaugeImages; // ゲージの表示用イメージ
    private float[] currentAmounts; // 各ゲージの現在の量

    private int UseLimit; // アイテム使用の回数制限
    private int UpdateImage; // 変更するゲージの指定

    public TextMeshProUGUI NoItem;
    public float fadeOutTime = 1.0f;
    private float currentAlpha;
    private float timer;

    private bool isAllGaugeResetting = false;
    private bool isMukiButtonResetting = false;

    private void Start()
    {
        NoItem.gameObject.SetActive(false); // アイテム回数制限のtext非表示

        currentAmounts = new float[gaugeImages.Length];

        for (int i = 0; i < currentAmounts.Length; i++)
        {
            currentAmounts[i] = 0f;
        }

        UpdateGaugeDisplay();

        currentAlpha = NoItem.alpha;
    }

    private void Update()
    {
        // タイマーを更新する
        timer += Time.deltaTime;

        // フェードアウト処理
        if (timer < fadeOutTime)
        {
            float normalizedTime = timer / fadeOutTime;
            float newAlpha = Mathf.Lerp(currentAlpha, 0f, normalizedTime);
            NoItem.alpha = newAlpha;
        }
        else
        {
            NoItem.enabled = true;
        }

        KiraGauge();

    }

    // ボタンが押された時に呼ばれる関数
    public void MukiButton()
    {
        UseLimit = DataManager.Instance.LoadInt("MukiCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("MukiCount", UseLimit);
            currentAmounts[0] += increaseAmounts[0]; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts[4]; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[0] - 1 >= maxAmounts[0])
            {
                currentAmounts[0] = 0f;
            }

            UpdateImage = 0;
            UpdateGaugeDisplay();

        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
    }

    public void OmoButton()
    {
        UseLimit = DataManager.Instance.LoadInt("OmoCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("OmoCount", UseLimit);
            currentAmounts[1] += increaseAmounts[1]; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts[4]; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[1] - 1 >= maxAmounts[1])
            {
                currentAmounts[1] = 0f;
            }

            UpdateImage = 1;
            UpdateGaugeDisplay();

        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
    }


    public void BetaButton()
    {
        UseLimit = DataManager.Instance.LoadInt("BetaCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("BetaCount", UseLimit);
            currentAmounts[2] += increaseAmounts[2]; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts[4]; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[2] - 1 >= maxAmounts[2])
            {
                currentAmounts[2] = 0f;
            }

            UpdateImage = 1;
            UpdateGaugeDisplay();

        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
    }

    public void PataButton()
    {
        UseLimit = DataManager.Instance.LoadInt("PataCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("PataCount", UseLimit);
            currentAmounts[3] += increaseAmounts[3]; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts[4]; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[3] - 1 >= maxAmounts[3])
            {
                currentAmounts[3] = 0f;
            }

            UpdateImage = 1;
            UpdateGaugeDisplay();

        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);

        }
    }


    /*StartCoroutineは、Unityのコルーチン（Coroutine）を開始するための特別な関数です。
    コルーチンは、一時停止・再開可能な関数のことで、非同期的な処理を実現するために使用されます。

    一般的なメソッドは、実行されると最後まで実行されるまで制御が戻らないのに対し、
    コルーチンは途中で一時停止し、あとで再開できる特殊なメソッドです。非同期的な処理をシンプルに実装する際に便利です。
    */


    //Debug用のButtton
    public void DebugIncreaseMukiGauge()
    {
        if (isMukiButtonResetting) // リセット中はボタンを無効化
            return;

        currentAmounts[0] += increaseAmounts[0];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[0] >= maxAmounts[0])
        {
            gaugeImages[0].fillAmount = 0f;
            StartCoroutine(ResetMukiGaugeAfterDelay());
        }

        UpdateImage = 0;
        UpdateGaugeDisplay();
    }

    public void DebugIncreaseOmoGauge()
    {
        currentAmounts[1] += increaseAmounts[1];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[1] >= maxAmounts[1])
        {
            currentAmounts[1] = 0f;
        }

        UpdateImage = 1;
        UpdateGaugeDisplay();
    }

    public void DebugIncreaseBetaGauge()
    {
        currentAmounts[2] += increaseAmounts[2];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[2] >= maxAmounts[2])
        {
            currentAmounts[2] = 0f;
        }

        UpdateImage = 2;
        UpdateGaugeDisplay();
    }

    public void DebugIncreasePataGauge()
    {
        currentAmounts[3] += increaseAmounts[3];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[3] >= maxAmounts[3])
        {
            currentAmounts[3] = 0f;
        }

        UpdateImage = 3;
        UpdateGaugeDisplay();
    }




    //MukiButtonがMAXになったら2秒後にリセットされる
    private IEnumerator ResetMukiGaugeAfterDelay()
    {
        isMukiButtonResetting = true;
        DebugMukiButton.interactable = false; // ボタンを無効化

        //yield return new WaitForSeconds(秒数)を使って一時停止をし、
        //指定した秒数後に再開されるようになっている。
        //(例 : アニメーション、待ち時間を含む処理、サーバーとの通信など)
        yield return new WaitForSeconds(2f);

        currentAmounts[0] = 0f;
        gaugeImages[0].fillAmount = 0f;

        DebugMukiButton.interactable = true; // ボタンを有効化
        isMukiButtonResetting = false;
    }





    public void KiraGauge()
    {

        // ゲージ量が最大値を超えた場合、ゲージをリセットする
        if (currentAmounts[4] >= maxAmounts[4] && !isAllGaugeResetting)
        {
            StartCoroutine(ResetMukiGaugeAfterDelay());
            StartCoroutine(ResetGaugeAfterDelay());
        }

        UpdateImage = 4;
        UpdateGaugeDisplay();
    }

    //KiraGaugeがMAXになったらすべてのゲージが2秒後にリセットされる
    private IEnumerator ResetGaugeAfterDelay()
    {
        isAllGaugeResetting = true;

        yield return new WaitForSeconds(2f);

        // すべてのゲージをリセット
        for (int i = 0; i < gaugeImages.Length; i++)
        {
            currentAmounts[i] = 0f;
            gaugeImages[i].fillAmount = 0f;
        }

        isAllGaugeResetting = false;
    }



    // ゲージの表示を更新する
    private void UpdateGaugeDisplay()
    {
        for (int i = 0; i < gaugeImages.Length; i++)
        {
            float normalizedAmount = currentAmounts[UpdateImage] / maxAmounts[UpdateImage];

            gaugeImages[UpdateImage].fillAmount = normalizedAmount;
        }
    }



}