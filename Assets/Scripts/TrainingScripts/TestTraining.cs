using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestTraining : MonoBehaviour
{
    public Button[] invalidButton;//MukiButtonを2秒後にリセットする変数の宣言

    public float increaseAmounts = 1; // 一回に増える増加量
    public float[] maxAmounts; // 最大量
    public Image[] gaugeImages; // ゲージの表示用イメージ
    public float[] currentAmounts; // 各ゲージの現在の量


    private int UseLimit; // アイテム使用の回数制限
    private int UpdateImage; // 変更するゲージの指定

    public TextMeshProUGUI NoItem;
    public float fadeOutTime = 1.0f;
    private float currentAlpha;
    private float timer;

    private int[] GaugeMaxCount; // 各ゲージがMaxになった回数
    private bool isKiraGaugeMax = false; // KiraGaugeがMAXになったフラグ

    public GameObject PlayerSprite;
    public Sprite[] EvolveSprite;

    private bool isAllGaugeResetting = false;
    private bool isMukiButtonResetting = false;
    private bool isOmoButtonResetting = false;
    private bool isBetaButtonResetting = false;
    private bool isPataButtonResetting = false;

    private void Start()
    {
        GaugeMaxCount = new int[invalidButton.Length];
        for (int i = 0; i < GaugeMaxCount.Length; i++)
        {
            GaugeMaxCount[i] = 0;
        }

        NoItem.gameObject.SetActive(false); // アイテム回数制限のtext非表示

        currentAmounts = new float[gaugeImages.Length];

        for (int i = 0; i < currentAmounts.Length; i++)
        {
            currentAmounts[i] = 0f;
        }

        UpdateGaugeDisplay();

        currentAlpha = NoItem.alpha;

        GaugeMaxCount[0] = DataManager.Instance.LoadInt("MMG");
        GaugeMaxCount[1] = DataManager.Instance.LoadInt("OMG");
        GaugeMaxCount[2] = DataManager.Instance.LoadInt("BMG");
        GaugeMaxCount[3] = DataManager.Instance.LoadInt("PMG");
        GaugeMaxCount[4] = DataManager.Instance.LoadInt("KMG");
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
        if (isMukiButtonResetting) // リセット中はボタンを無効化
            return;

        UseLimit = DataManager.Instance.LoadInt("MukiCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("MukiCount", UseLimit);
            currentAmounts[0] += increaseAmounts; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[0] - 1 >= maxAmounts[0])
            {
                gaugeImages[0].fillAmount = 0f;
                StartCoroutine(ResetMukiGauge());
            }
            UpdateImage = 0;
            UpdateGaugeDisplay();
        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
        //PlayParticle(0); // パーティクル再生処理を呼び出す
    }

    public void OmoButton()
    {
        if (isOmoButtonResetting) // リセット中はボタンを無効化
            return;

        UseLimit = DataManager.Instance.LoadInt("OmoCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("OmoCount", UseLimit);
            currentAmounts[1] += increaseAmounts; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[1] - 1 >= maxAmounts[1])
            {
                gaugeImages[1].fillAmount = 0f;
                StartCoroutine(ResetOmoGauge());
            }
            UpdateImage = 1;
            UpdateGaugeDisplay();
        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
        //PlayParticle(1); // パーティクル再生処理を呼び出す
    }


    public void BetaButton()
    {
        if (isBetaButtonResetting) // リセット中はボタンを無効化
            return;

        UseLimit = DataManager.Instance.LoadInt("BetaCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("BetaCount", UseLimit);
            currentAmounts[2] += increaseAmounts; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[2] - 1 >= maxAmounts[2])
            {
                gaugeImages[2].fillAmount = 0f;
                StartCoroutine(ResetBetaGauge());
            }
            UpdateImage = 2;
            UpdateGaugeDisplay();
        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
        //PlayParticle(2); // パーティクル再生処理を呼び出す
    }

    public void PataButton()
    {
        if (isPataButtonResetting) // リセット中はボタンを無効化
            return;

        UseLimit = DataManager.Instance.LoadInt("PataCount");
        if (UseLimit >= 1)
        {
            UseLimit -= 1;
            DataManager.Instance.SaveInt("PataCount", UseLimit);
            currentAmounts[3] += increaseAmounts; // ゲージ量を増やす
            currentAmounts[4] += increaseAmounts; // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (currentAmounts[3] - 1 >= maxAmounts[3])
            {
                gaugeImages[3].fillAmount = 0f;
                StartCoroutine(ResetPataGauge());
            }
            UpdateImage = 3;
            UpdateGaugeDisplay();
        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
        //PlayParticle(3); // パーティクル再生処理を呼び出す
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
        }

        UpdateImage = 4;
        UpdateGaugeDisplay();

        SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
        targetSpriteRenderer.sprite = EvolveSprite[0];

        // KiraGaugeがMAXではない場合の処理
        if (currentAmounts[4] < maxAmounts[4] && isKiraGaugeMax)
        {
            isKiraGaugeMax = false;
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

    /*
    private void PlayParticle(int particleIndex)
    {
        if (particleIndex >= 0 && particleIndex < particleSystems.Length)
        {
            particleSystems[particleIndex].Play();
        }
    }*/


    //GaugeがMAXになったら2秒後にリセットされる
    private IEnumerator ResetMukiGauge()
    {
        GaugeMaxCount[0] += 1;
        DataManager.Instance.SaveInt("MMG", GaugeMaxCount[0]);

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
        GaugeMaxCount[1] += 1;
        DataManager.Instance.SaveInt("OMG", GaugeMaxCount[1]);

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
        GaugeMaxCount[2] += 1;
        DataManager.Instance.SaveInt("BMG", GaugeMaxCount[2]);

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
        GaugeMaxCount[3] += 1;
        DataManager.Instance.SaveInt("PMG", GaugeMaxCount[3]);

        isPataButtonResetting = true;
        invalidButton[3].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        currentAmounts[3] = 0f;
        gaugeImages[3].fillAmount = 0f;

        invalidButton[3].interactable = true; // ボタンを有効化
        isPataButtonResetting = false;
    }

    //KiraGaugeがMAXになったらすべてのゲージが2秒後にリセットされる
    private IEnumerator AllResetGauge()
    {
        GaugeMaxCount[4] += 1;
        DataManager.Instance.SaveInt("KMG", GaugeMaxCount[4]);

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
}
