using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestTraining : MonoBehaviour
{
    public Button[] invalidButton;//MukiButtonを2秒後にリセットする変数の宣言

    public float increaseAmounts = 1; // 一回に増える増加量
    public Image[] gaugeImages; // ゲージの表示用イメージ
    public float[] currentAmounts; // 各ゲージの現在の量


    private int UseLimit; // アイテム使用の回数制限

    public TextMeshProUGUI NoItem;
    public float fadeOutTime = 1.0f;
    private float currentAlpha;
    private float timer;

    public int[] GaugeMaxCount; // 各ゲージがMaxになった回数
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
        NoItem.gameObject.SetActive(false); // アイテム回数制限のtext非表示

        currentAmounts = new float[gaugeImages.Length];

        for (int i = 0; i < currentAmounts.Length; i++)
        {
            currentAmounts[i] = 0f;
        }

        UpdateGaugeDisplayM();
        UpdateGaugeDisplayO();
        UpdateGaugeDisplayB();
        UpdateGaugeDisplayP();
        UpdateGaugeDisplayK();

        currentAlpha = NoItem.alpha;

        GaugeMaxCount[0] = DataManager.Instance.LoadInt("MCMG");
        GaugeMaxCount[1] = DataManager.Instance.LoadInt("OCMG");
        GaugeMaxCount[2] = DataManager.Instance.LoadInt("BCMG");
        GaugeMaxCount[3] = DataManager.Instance.LoadInt("PCMG");
        GaugeMaxCount[4] = DataManager.Instance.LoadInt("KCMG");

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

        if (DataManager.Instance.LoadInt("KCMG") == 0)
        {
            DataManager.Instance.SaveInt("MMG", 10);
            DataManager.Instance.SaveInt("OMG", 10);
            DataManager.Instance.SaveInt("BMG", 10);
            DataManager.Instance.SaveInt("PMG", 10);
            DataManager.Instance.SaveInt("KMG", 30);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 1)
        {
            DataManager.Instance.SaveInt("MMG", 20);
            DataManager.Instance.SaveInt("OMG", 20);
            DataManager.Instance.SaveInt("BMG", 20);
            DataManager.Instance.SaveInt("PMG", 20);
            DataManager.Instance.SaveInt("KMG", 60);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 2)
        {
            DataManager.Instance.SaveInt("MMG", 30);
            DataManager.Instance.SaveInt("OMG", 30);
            DataManager.Instance.SaveInt("BMG", 30);
            DataManager.Instance.SaveInt("PMG", 30);
            DataManager.Instance.SaveInt("KMG", 90);
        }

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
            if (currentAmounts[0] - 1 >= DataManager.Instance.LoadInt("MMG"))
            {
                StartCoroutine(ResetMukiGauge());
                GaugeMaxCount[0] += 1;
                DataManager.Instance.SaveInt("MCMG", GaugeMaxCount[0]);
            }
            UpdateGaugeDisplayM();
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
            if (currentAmounts[1] - 1 >= DataManager.Instance.LoadInt("OMG"))
            {
                StartCoroutine(ResetOmoGauge());
                GaugeMaxCount[1] += 1;
                DataManager.Instance.SaveInt("OCMG", GaugeMaxCount[1]);
            }
            UpdateGaugeDisplayO();
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
            if (currentAmounts[2] - 1 >= DataManager.Instance.LoadInt("BMG"))
            {
                StartCoroutine(ResetBetaGauge());
                GaugeMaxCount[2] += 1;
                DataManager.Instance.SaveInt("BCMG", GaugeMaxCount[2]);
            }
            UpdateGaugeDisplayB();
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
            if (currentAmounts[3] - 1 >= DataManager.Instance.LoadInt("PMG"))
            {
                StartCoroutine(ResetPataGauge());
                GaugeMaxCount[3] += 1;
                DataManager.Instance.SaveInt("PCMG", GaugeMaxCount[3]);

            }
            UpdateGaugeDisplayP();
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
        if (currentAmounts[4] - 1 >= DataManager.Instance.LoadInt("KMG") && !isAllGaugeResetting && !isKiraGaugeMax)
        {
            StartCoroutine(ResetMukiGauge());
            StartCoroutine(ResetOmoGauge());
            StartCoroutine(ResetBetaGauge());
            StartCoroutine(ResetPataGauge());
            StartCoroutine(AllResetGauge());

            GaugeMaxCount[4] += 1;
            DataManager.Instance.SaveInt("KCMG", GaugeMaxCount[4]);

            isKiraGaugeMax = true;
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[0];

        }

        UpdateGaugeDisplayM();
        UpdateGaugeDisplayO();
        UpdateGaugeDisplayB();
        UpdateGaugeDisplayP();
        UpdateGaugeDisplayK();

        // KiraGaugeがMAXではない場合の処理
        if (currentAmounts[4] < DataManager.Instance.LoadInt("KMG") && isKiraGaugeMax)
        {
            isKiraGaugeMax = false;
        }
    }


    // ゲージの表示を更新する
    private void UpdateGaugeDisplayM()
    {
        float normalizedAmount = currentAmounts[0] / DataManager.Instance.LoadInt("MMG");

        gaugeImages[0].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayO()
    {
        float normalizedAmount = currentAmounts[1] / DataManager.Instance.LoadInt("OMG");

        gaugeImages[1].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayB()
    {
        
        float normalizedAmount = currentAmounts[2] / DataManager.Instance.LoadInt("BMG");

        gaugeImages[2].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayP()
    {
        float normalizedAmount = currentAmounts[3] / DataManager.Instance.LoadInt("PMG");

        gaugeImages[3].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayK()
    {
        float normalizedAmount = currentAmounts[4] / DataManager.Instance.LoadInt("KMG");

        gaugeImages[4].fillAmount = normalizedAmount;
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
}
