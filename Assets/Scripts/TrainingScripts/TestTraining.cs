﻿using System.Collections;
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

        UpdateGaugeDisplayM();
        UpdateGaugeDisplayO();
        UpdateGaugeDisplayB();
        UpdateGaugeDisplayP();
        UpdateGaugeDisplayK();

        currentAlpha = NoItem.alpha;

        DataManager.Instance.SaveInt("MMG", 10);
        DataManager.Instance.SaveInt("OMG", 10);
        DataManager.Instance.SaveInt("BMG", 10);
        DataManager.Instance.SaveInt("PMG", 10);

        GaugeMaxCount[0] = DataManager.Instance.LoadInt("MCMG");
        GaugeMaxCount[1] = DataManager.Instance.LoadInt("OCMG");
        GaugeMaxCount[2] = DataManager.Instance.LoadInt("BCMG");
        GaugeMaxCount[3] = DataManager.Instance.LoadInt("PCMG");
        GaugeMaxCount[4] = DataManager.Instance.LoadInt("KCMG");

        if (DataManager.Instance.LoadBool("E1F") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[0];
        }
        else if (DataManager.Instance.LoadBool("E1O") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[1];
        }

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
            DataManager.Instance.SaveInt("KMG", 30);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 1)
        {
            DataManager.Instance.SaveInt("KMG", 50);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 2)
        {
            DataManager.Instance.SaveInt("KMG", 70);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 3)
        {
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
            DataManager.Instance.SaveFloat("FG",DataManager.Instance.LoadFloat("FG") + increaseAmounts); // ゲージ量を増やす
            DataManager.Instance.SaveFloat("KG",DataManager.Instance.LoadFloat("KG") + increaseAmounts); // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (DataManager.Instance.LoadFloat("FG") >= DataManager.Instance.LoadInt("MMG"))
            {
                StartCoroutine(ResetMukiGauge());
                GaugeMaxCount[0] += 1;
                DataManager.Instance.SaveInt("MCMG", GaugeMaxCount[0]);
                DataManager.Instance.SaveBool("Muki", true);
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
            DataManager.Instance.SaveFloat("OG", DataManager.Instance.LoadFloat("OG") + increaseAmounts); // ゲージ量を増やす
            DataManager.Instance.SaveFloat("KG",DataManager.Instance.LoadFloat("KG") + increaseAmounts); // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (DataManager.Instance.LoadFloat("OG") >= DataManager.Instance.LoadInt("OMG"))
            {
                StartCoroutine(ResetOmoGauge());
                GaugeMaxCount[1] += 1;
                DataManager.Instance.SaveInt("OCMG", GaugeMaxCount[1]);
                DataManager.Instance.SaveBool("Omo", true);
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
            DataManager.Instance.SaveFloat("BG", DataManager.Instance.LoadFloat("BG") + increaseAmounts); // ゲージ量を増やす
            DataManager.Instance.SaveFloat("KG",DataManager.Instance.LoadFloat("KG") + increaseAmounts); // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (DataManager.Instance.LoadFloat("BG") >= DataManager.Instance.LoadInt("BMG"))
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
            DataManager.Instance.SaveFloat("PG", DataManager.Instance.LoadFloat("PG") + increaseAmounts); // ゲージ量を増やす
            DataManager.Instance.SaveFloat("KG",DataManager.Instance.LoadFloat("KG") + increaseAmounts); // きらめきゲージ量を増やす

            // ゲージ量が最大値を超えた場合、ゲージをリセットする
            if (DataManager.Instance.LoadFloat("PG") >= DataManager.Instance.LoadInt("PMG"))
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
        if (DataManager.Instance.LoadFloat("KG") >= DataManager.Instance.LoadInt("KMG") && !isAllGaugeResetting && !isKiraGaugeMax)
        {
            StartCoroutine(ResetMukiGauge());
            StartCoroutine(ResetOmoGauge());
            StartCoroutine(ResetBetaGauge());
            StartCoroutine(ResetPataGauge());
            StartCoroutine(AllResetGauge());

            isKiraGaugeMax = true;
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            if (DataManager.Instance.LoadInt("KCMG") > DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1") == false)
            {
                targetSpriteRenderer.sprite = EvolveSprite[0];
                MaxCountReset();
                DataManager.Instance.SaveBool("E1",true);
                DataManager.Instance.SaveBool("E1F", true);
            }
            else if (DataManager.Instance.LoadInt("KCMG") < DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1") == false)
            {
                targetSpriteRenderer.sprite = EvolveSprite[1];
                MaxCountReset();
                DataManager.Instance.SaveBool("E1", true);
                DataManager.Instance.SaveBool("E1O", true);
            }
            else if (DataManager.Instance.LoadInt("KCMG") == DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1") == false)
            {
                int randomIndex = Random.Range(0, 2);
                if (randomIndex == 0)
                {
                    targetSpriteRenderer.sprite = EvolveSprite[0];
                    MaxCountReset();
                    DataManager.Instance.SaveBool("E1", true);
                    DataManager.Instance.SaveBool("E1F", true);
                }
                else
                {
                    targetSpriteRenderer.sprite = EvolveSprite[1];
                    MaxCountReset();
                    DataManager.Instance.SaveBool("E1", true);
                    DataManager.Instance.SaveBool("E1O", true);
                }
            }

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

    public void MaxCountReset()
    {
        DataManager.Instance.SaveInt("MCMG", 0);
        DataManager.Instance.SaveInt("MCMG", 0);
        DataManager.Instance.SaveInt("MCMG", 0);
        DataManager.Instance.SaveInt("MCMG", 0);

        GaugeMaxCount[0] = DataManager.Instance.LoadInt("MCMG");
        GaugeMaxCount[1] = DataManager.Instance.LoadInt("OCMG");
        GaugeMaxCount[2] = DataManager.Instance.LoadInt("BCMG");
        GaugeMaxCount[3] = DataManager.Instance.LoadInt("PCMG");
    }


    // ゲージの表示を更新する
    private void UpdateGaugeDisplayM()
    {
        float normalizedAmount = DataManager.Instance.LoadFloat("FG") / DataManager.Instance.LoadInt("MMG");

        gaugeImages[0].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayO()
    {
        float normalizedAmount = DataManager.Instance.LoadFloat("OG") / DataManager.Instance.LoadInt("OMG");

        gaugeImages[1].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayB()
    {
        
        float normalizedAmount = DataManager.Instance.LoadFloat("BG") / DataManager.Instance.LoadInt("BMG");

        gaugeImages[2].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayP()
    {
        float normalizedAmount = DataManager.Instance.LoadFloat("PG") / DataManager.Instance.LoadInt("PMG");

        gaugeImages[3].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayK()
    {
        float normalizedAmount = DataManager.Instance.LoadFloat("KG") / DataManager.Instance.LoadInt("KMG");

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

        DataManager.Instance.SaveFloat("FG", 0);
        gaugeImages[0].fillAmount = 0f;

        invalidButton[0].interactable = true; // ボタンを有効化
        isMukiButtonResetting = false;
    }

    private IEnumerator ResetOmoGauge()
    {
        isOmoButtonResetting = true;
        invalidButton[1].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("OG", 0);
        gaugeImages[1].fillAmount = 0f;

        invalidButton[1].interactable = true; // ボタンを有効化
        isOmoButtonResetting = false;
    }

    private IEnumerator ResetBetaGauge()
    {
        isBetaButtonResetting = true;
        invalidButton[2].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("BG",0);
        gaugeImages[2].fillAmount = 0f;

        invalidButton[2].interactable = true; // ボタンを有効化
        isBetaButtonResetting = false;
    }

    private IEnumerator ResetPataGauge()
    {
        isPataButtonResetting = true;
        invalidButton[3].interactable = false; // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("PG",0);
        gaugeImages[3].fillAmount = 0f;

        invalidButton[3].interactable = true; // ボタンを有効化
        isPataButtonResetting = false;
    }

    //KiraGaugeがMAXになったらすべてのゲージが2秒後にリセットされる
    private IEnumerator AllResetGauge()
    {
        isAllGaugeResetting = true;

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("FG", 0);
        DataManager.Instance.SaveFloat("OG", 0);
        DataManager.Instance.SaveFloat("BG", 0);
        DataManager.Instance.SaveFloat("PG", 0);
        DataManager.Instance.SaveFloat("KG", 0);
        // すべてのゲージをリセット
        for (int i = 0; i < gaugeImages.Length; i++)
        {
            gaugeImages[i].fillAmount = 0f;
        }

        isAllGaugeResetting = false;
        GaugeMaxCount[4] += 1;
        DataManager.Instance.SaveInt("KCMG", GaugeMaxCount[4]);
    }
}