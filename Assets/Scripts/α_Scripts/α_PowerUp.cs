using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class α_PowerUp : MonoBehaviour
{
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

    private void Start()
    {
        NoItem.gameObject.SetActive(false); // アイテム回数制限のtext非表示

        //ResetGenerator();

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
                //GenerateNextSprite();
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

    public void KiraGauge()
    {
        // ゲージ量が最大値を超えた場合、ゲージをリセットする
        if (currentAmounts[4] - 1 >= maxAmounts[4])
        {
            currentAmounts[4] = 0f;
        }

        UpdateImage = 4;
        UpdateGaugeDisplay();
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