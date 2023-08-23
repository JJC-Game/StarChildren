using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugButton : MonoBehaviour
{
    public Button[] invalidButton;//MukiButtonを2秒後にリセットする変数の宣言

    public float[] increaseAmounts; // 一回に増える増加量
    public float[] maxAmounts; // 最大量
    public Image[] gaugeImages; // ゲージの表示用イメージ
    private float[] currentAmounts; // 各ゲージの現在の量

    //private int UseLimit; // アイテム使用の回数制限
    private int UpdateImage; // 変更するゲージの指定

    public TextMeshProUGUI NoItem;
    public float fadeOutTime = 1.0f;
    private float currentAlpha;
    private float timer;


    public Sprite[] kiraMaxSprites; // 新しいスプライトを格納する配列
    public Image kiraMaxSpriteDisplay; // 新しいスプライトの表示に使用するImageコンポーネント
    private int[] buttonPressCount; // 各ボタンの押された回数を格納する配列
    private int mostPressedButtonIndex; // 最も押されたボタンのインデックス
    private bool isKiraGaugeMax = false; // KiraGaugeがMAXになったフラグ


    private bool isAllGaugeResetting = false;
    private bool isMukiButtonResetting = false;
    private bool isOmoButtonResetting = false;
    private bool isBetaButtonResetting = false;
    private bool isPataButtonResetting = false;

    private void Start()
    {
        // ボタンの押された回数を初期化
        buttonPressCount = new int[invalidButton.Length];
        for (int i = 0; i < buttonPressCount.Length; i++)
        {
            buttonPressCount[i] = 0;
        }

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

    //Debug用のButtton
    public void DebugMukiGauge()
    {
        if (isMukiButtonResetting) // リセット中はボタンを無効化
            return;

        currentAmounts[0] += increaseAmounts[0];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[0] >= maxAmounts[0])
        {
            gaugeImages[0].fillAmount = 0f;
            StartCoroutine(ResetMukiGauge());
        }

        UpdateImage = 0;
        UpdateGaugeDisplay();

        buttonPressCount[0]++;
    }

    public void DebugOmoGauge()
    {
        if (isOmoButtonResetting) // リセット中はボタンを無効化
            return;

        currentAmounts[1] += increaseAmounts[1];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[1] >= maxAmounts[1])
        {
            gaugeImages[1].fillAmount = 0f;
            StartCoroutine(ResetOmoGauge());
        }

        UpdateImage = 1;
        UpdateGaugeDisplay();

        buttonPressCount[1]++;

    }

    public void DebugBetaGauge()
    {
        if (isBetaButtonResetting) // リセット中はボタンを無効化
            return;

        currentAmounts[2] += increaseAmounts[2];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[2] >= maxAmounts[2])
        {
            gaugeImages[2].fillAmount = 0f;
            StartCoroutine(ResetBetaGauge());
        }

        UpdateImage = 2;
        UpdateGaugeDisplay();

        buttonPressCount[2]++;
    }

    public void DebugPataGauge()
    {
        if (isPataButtonResetting) // リセット中はボタンを無効化
            return;

        currentAmounts[3] += increaseAmounts[3];
        currentAmounts[4] += increaseAmounts[4];

        if (currentAmounts[3] >= maxAmounts[3])
        {
            gaugeImages[3].fillAmount = 0f;
            StartCoroutine(ResetPataGauge());
        }

        UpdateImage = 3;
        UpdateGaugeDisplay();

        buttonPressCount[3]++;
    }
    

    public void KiraGauge()
    {

        // ゲージ量が最大値を超えた場合、ゲージをリセットする
        if (currentAmounts[4] >= maxAmounts[4] && !isAllGaugeResetting && !isKiraGaugeMax)
        {
            StartCoroutine(ResetMukiGauge());
            StartCoroutine(ResetOmoGauge());
            StartCoroutine(ResetBetaGauge());
            StartCoroutine(ResetPataGauge());
            StartCoroutine(AllResetGauge());

            isKiraGaugeMax = true;
            UpdateMostPressedButton();
        }

        UpdateImage = 4;
        UpdateGaugeDisplay();

        // KiraGaugeがMAXではない場合の処理
        if (currentAmounts[4] < maxAmounts[4] && isKiraGaugeMax)
        {
            isKiraGaugeMax = false;
            ResetMostPressedButton();
        }
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


    private void UpdateMostPressedButton()
    {
        // 最も押された回数をカウント
        int maxPressCount = 0;
        mostPressedButtonIndex = 0;

        for (int i = 0; i < buttonPressCount.Length; i++)
        {
            if (buttonPressCount[i] > maxPressCount)
            {
                maxPressCount = buttonPressCount[i];
                mostPressedButtonIndex = i;
            }
        }

        // 最も押されたボタンに応じて新しいスプライトを表示
        if (mostPressedButtonIndex < kiraMaxSprites.Length)
        {
            kiraMaxSpriteDisplay.sprite = kiraMaxSprites[mostPressedButtonIndex];
            E
        }
    }

    private void ResetMostPressedButton()
    {
        // ボタンの押された回数をリセット
        for (int i = 0; i < buttonPressCount.Length; i++)
        {
            buttonPressCount[i] = 0;
        }
    }


    //KiraGaugeがMAXになったらすべてのゲージが2秒後にリセットされる
    private IEnumerator AllResetGauge()
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


    //GaugeがMAXになったら2秒後にリセットされる
    private IEnumerator ResetMukiGauge()
    {
        isMukiButtonResetting = true;
        invalidButton[0].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        currentAmounts[0] = 0f;
        gaugeImages[0].fillAmount = 0f;

        invalidButton[0].interactable = true; // ボタンを有効化
        isMukiButtonResetting = false;
    }

    private IEnumerator ResetOmoGauge()
    {
        isOmoButtonResetting = true;
        invalidButton[1].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        currentAmounts[1] = 0f;
        gaugeImages[1].fillAmount = 0f;

        invalidButton[1].interactable = true; // ボタンを有効化
        isOmoButtonResetting = false;
    }

    private IEnumerator ResetBetaGauge()
    {
        isBetaButtonResetting = true;
        invalidButton[2].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        currentAmounts[2] = 0f;
        gaugeImages[2].fillAmount = 0f;

        invalidButton[2].interactable = true; // ボタンを有効化
        isBetaButtonResetting = false;
    }

    private IEnumerator ResetPataGauge()
    {
        isPataButtonResetting = true;
        invalidButton[3].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        currentAmounts[3] = 0f;
        gaugeImages[3].fillAmount = 0f;

        invalidButton[3].interactable = true; // ボタンを有効化
        isPataButtonResetting = false;
    }
}