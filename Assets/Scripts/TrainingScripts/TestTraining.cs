using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TestTraining : MonoBehaviour
{
    public Button[] invalidButton;//MukiButtonを2秒後にリセットする変数の宣言

    // ゲージ関連
    public float increaseAmounts = 1; // 一回に増える増加量
    public Image[] gaugeImages; // ゲージの表示用イメージ
    public float[] currentAmounts; // 各ゲージの現在の量
    public int[] GaugeMaxCount; // 各ゲージがMaxになった回数
    private bool isKiraGaugeMax = false; // KiraGaugeがMAXになったフラグ

    public int kiraMax0;
    public int kiraMax1;
    public int kiraMax2;
    public int kiraMax3;

    public GameObject PlayerSprite; //キャラクターの見た目
    public Sprite[] EvolveSprite; //進化後のキャラクターの見た目

    public GameObject MainCanvas;
    public GameObject FinishCanvas;
    public GameObject KakuninCanvas;
    public GameObject ResetCheckCanvas;
    private bool Finish;
    private bool Reset;

    private int UseLimit; // アイテム使用の回数制限

    // アイテムがないときのtext表示関連
    public TextMeshProUGUI NoItem; // アイテムがないときに表示するtext
    public float fadeOutTime = 1.0f; // 上のtextが消える時間
    private float currentAlpha;
    private float timer;

    // ゲージリセット時のフラグ
    private bool isAllGaugeResetting = false;
    private bool isMukiButtonResetting = false;
    private bool isOmoButtonResetting = false;
    //private bool isBetaButtonResetting = false;
    //private bool isPataButtonResetting = false;

    public Transform MukiMaxEffect;
    public Transform OmoMaxEffect;
    public Transform KiraMaxEffect;
    public Transform ImageChengeEffect;

    private void Start()
    {
        NoItem.gameObject.SetActive(false); // アイテム回数制限のtext非表示
        FinishCanvas.gameObject.SetActive(false); // 終了ボタンの非表示
        KakuninCanvas.gameObject.SetActive(false);
        ResetCheckCanvas.gameObject.SetActive(false);
        Finish = false;
        Reset = false;

        currentAmounts = new float[gaugeImages.Length];

        UpdateGaugeDisplayM();
        UpdateGaugeDisplayO();
        //UpdateGaugeDisplayB();
        //UpdateGaugeDisplayP();
        UpdateGaugeDisplayK();

        currentAlpha = NoItem.alpha;

        if (DataManager.Instance.LoadInt("KCMG") == 0)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax0);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 1)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax1);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 2)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax2);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 3)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax3);
        }

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
        else if (DataManager.Instance.LoadBool("E2FF") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[2];
        }
        else if (DataManager.Instance.LoadBool("E2OO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[3];
        }
        else if (DataManager.Instance.LoadBool("E2FO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[4];
        }
        else if (DataManager.Instance.LoadBool("E3FFF") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[5];
        }
        else if (DataManager.Instance.LoadBool("E3OOO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[6];
        }
        else if (DataManager.Instance.LoadBool("E3FFO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[7];
        }
        else if (DataManager.Instance.LoadBool("E3FOO") == true)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[8];
        }
        else if (DataManager.Instance.LoadBool("E1") == false)
        {
            SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
            targetSpriteRenderer.sprite = EvolveSprite[9];
        }

        if(DataManager.Instance.LoadBool("Finish") == true)
        {
            MainCanvas.gameObject.SetActive(false);
            FinishCanvas.gameObject.SetActive(true);
            MainCanvas.gameObject.SetActive(false);
            KakuninCanvas.gameObject.SetActive(false);
        }

        if(DataManager.Instance.LoadBool("ClearReset"))
        {
            AllReset();
            DataManager.Instance.SaveBool("ClearReset", false);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
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
            DataManager.Instance.SaveInt("KMG", kiraMax0);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 1)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax1);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 2)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax2);
        }
        else if (DataManager.Instance.LoadInt("KCMG") == 3)
        {
            DataManager.Instance.SaveInt("KMG", kiraMax3);
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
                EffectManager.Instance.PlayEffect(0, MukiMaxEffect);
            }
            UpdateGaugeDisplayM();
        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
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
                EffectManager.Instance.PlayEffect(1, OmoMaxEffect);
            }
            UpdateGaugeDisplayO();
        }
        else
        {
            timer = 0f;
            NoItem.gameObject.SetActive(true);
        }
    }


    /*public void BetaButton()
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
    }*/

    public void KiraGauge()
    {
        // ゲージ量が最大値を超えた場合、ゲージをリセットする
        if (DataManager.Instance.LoadFloat("KG") >= DataManager.Instance.LoadInt("KMG") && !isAllGaugeResetting && !isKiraGaugeMax)
        {
            StartCoroutine(ResetMukiGauge());
            StartCoroutine(ResetOmoGauge());
            //StartCoroutine(ResetBetaGauge());
            //StartCoroutine(ResetPataGauge());
            StartCoroutine(AllResetGauge());

            isKiraGaugeMax = true;
            if (DataManager.Instance.LoadBool("E1") == false)
            {
                EVO1();
            }
            else if (DataManager.Instance.LoadBool("E1") == true && DataManager.Instance.LoadBool("E2") == false)
            {
                EVO2();
            }
            else if (DataManager.Instance.LoadBool("E1") == true && DataManager.Instance.LoadBool("E2") == true && DataManager.Instance.LoadBool("E3") == false)
            {
                EVO3();
            }
            else if (DataManager.Instance.LoadBool("E1") == true && DataManager.Instance.LoadBool("E2") == true && DataManager.Instance.LoadBool("E3") == true && DataManager.Instance.LoadBool("E4") == false)
            {
                UpdateGaugeDisplayM();
                UpdateGaugeDisplayO();
                //UpdateGaugeDisplayB();
                //UpdateGaugeDisplayP();
                UpdateGaugeDisplayK();
                EVOF();
            }
            EffectManager.Instance.PlayEffect(2, KiraMaxEffect);
        }

        UpdateGaugeDisplayM();
        UpdateGaugeDisplayO();
        //UpdateGaugeDisplayB();
        //UpdateGaugeDisplayP();
        UpdateGaugeDisplayK();

        // KiraGaugeがMAXではない場合の処理
        if (currentAmounts[4] < DataManager.Instance.LoadInt("KMG") && isKiraGaugeMax)
        {
            isKiraGaugeMax = false;
        }
    }

    public void EVO1()
    {
        SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
        // １回目の進化
        if (DataManager.Instance.LoadInt("MCMG") > DataManager.Instance.LoadInt("OCMG"))
        {
            targetSpriteRenderer.sprite = EvolveSprite[0];
            MaxCountReset();
            DataManager.Instance.SaveBool("E1", true);
            DataManager.Instance.SaveBool("E1F", true);
            DataManager.Instance.SaveBool("Muki", true);
        }
        else if (DataManager.Instance.LoadInt("MCMG") < DataManager.Instance.LoadInt("OCMG"))
        {
            targetSpriteRenderer.sprite = EvolveSprite[1];
            MaxCountReset();
            DataManager.Instance.SaveBool("E1", true);
            DataManager.Instance.SaveBool("E1O", true);
            DataManager.Instance.SaveBool("Omo", true);
        }
        else if (DataManager.Instance.LoadInt("MCMG") == DataManager.Instance.LoadInt("OCMG"))
        {
            int randomIndex = Random.Range(0, 2);
            if (randomIndex == 0)
            {
                targetSpriteRenderer.sprite = EvolveSprite[0];
                MaxCountReset();
                DataManager.Instance.SaveBool("E1", true);
                DataManager.Instance.SaveBool("E1F", true);
                DataManager.Instance.SaveBool("Muki", true);
            }
            else
            {
                targetSpriteRenderer.sprite = EvolveSprite[1];
                MaxCountReset();
                DataManager.Instance.SaveBool("E1", true);
                DataManager.Instance.SaveBool("E1O", true);
                DataManager.Instance.SaveBool("Omo", true);
            }
        }
        EffectManager.Instance.PlayEffect(3, ImageChengeEffect);
    }

    public void EVO2()
    {
        SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
        // ２回目の進化
        if (DataManager.Instance.LoadInt("MCMG") > DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1F") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[2];
            MaxCountReset();
            DataManager.Instance.SaveBool("E2", true);
            DataManager.Instance.SaveBool("E2FF", true);
            DataManager.Instance.SaveBool("E1F", false);
        }
        else if (DataManager.Instance.LoadInt("MCMG") < DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1O") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[3];
            MaxCountReset();
            DataManager.Instance.SaveBool("E2", true);
            DataManager.Instance.SaveBool("E2OO", true);
            DataManager.Instance.SaveBool("E1O", false);
        }
        else if (DataManager.Instance.LoadInt("MCMG") < DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1F") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[2];
            MaxCountReset();
            DataManager.Instance.SaveBool("E2", true);
            DataManager.Instance.SaveBool("E2FO", true);
            DataManager.Instance.SaveBool("E1F", false);
        }
        else if (DataManager.Instance.LoadInt("MCMG") > DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1O") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[3];
            MaxCountReset();
            DataManager.Instance.SaveBool("E2", true);
            DataManager.Instance.SaveBool("E2FO", true);
            DataManager.Instance.SaveBool("E1O", false);
        }
        else if (DataManager.Instance.LoadInt("MCMG") == DataManager.Instance.LoadInt("OCMG") && (DataManager.Instance.LoadBool("E1F") == true || DataManager.Instance.LoadInt("MCMG") == DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E1O") == true))
        {
            targetSpriteRenderer.sprite = EvolveSprite[4];
            MaxCountReset();
            DataManager.Instance.SaveBool("E2", true);
            DataManager.Instance.SaveBool("E2FO", true);
            DataManager.Instance.SaveBool("E1F", false);
            DataManager.Instance.SaveBool("E1O", false);
            DataManager.Instance.SaveBool("Omo", true);
            DataManager.Instance.SaveBool("Muki", true);
        }
        EffectManager.Instance.PlayEffect(3, ImageChengeEffect);
    }

    public void EVO3()
    {
        SpriteRenderer targetSpriteRenderer = PlayerSprite.GetComponent<SpriteRenderer>();
        // ３回目の進化
        if (DataManager.Instance.LoadInt("MCMG") >= DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E2FF") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[5];
            MaxCountReset();
            DataManager.Instance.SaveBool("E3", true);
            DataManager.Instance.SaveBool("E3FFF", true);
            DataManager.Instance.SaveBool("E2FF", false);
        }
        else if (DataManager.Instance.LoadInt("MCMG") <= DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E2OO") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[6];
            MaxCountReset();
            DataManager.Instance.SaveBool("E3", true);
            DataManager.Instance.SaveBool("E3OOO", true);
            DataManager.Instance.SaveBool("E2OO", false);
        }
        else if (DataManager.Instance.LoadInt("MCMG") >= DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E2FO") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[7];
            MaxCountReset();
            DataManager.Instance.SaveBool("E3", true);
            DataManager.Instance.SaveBool("E3FFO", true);
            DataManager.Instance.SaveBool("E2FO", false);
            DataManager.Instance.SaveBool("Omo", true);
            DataManager.Instance.SaveBool("Muki", true);
        }
        else if (DataManager.Instance.LoadInt("MCMG") <= DataManager.Instance.LoadInt("OCMG") && DataManager.Instance.LoadBool("E2FO") == true)
        {
            targetSpriteRenderer.sprite = EvolveSprite[8];
            MaxCountReset();
            DataManager.Instance.SaveBool("E3", true);
            DataManager.Instance.SaveBool("E3FOO", true);
            DataManager.Instance.SaveBool("E2FO", false);
            DataManager.Instance.SaveBool("Omo", true);
            DataManager.Instance.SaveBool("Muki", true);
        }
        EffectManager.Instance.PlayEffect(3, ImageChengeEffect);
    }

    public void EVOF()
    {
        DataManager.Instance.SaveBool("Finish", true);

        MainCanvas.gameObject.SetActive(false);
        FinishCanvas.gameObject.SetActive(true);
    }

    public void Kakunin()
    {
        FinishCanvas.gameObject.SetActive(false);
        KakuninCanvas.gameObject.SetActive(true);
        Finish = true;
    }

    public void Return()
    {
        if (Finish)
        {
            KakuninCanvas.gameObject.SetActive(false);
            FinishCanvas.gameObject.SetActive(true);
            Finish = false;
        }

        if (Reset)
        {
            ResetCheckCanvas.gameObject.SetActive(false);
            MainCanvas.gameObject.SetActive(true);
            Reset = false;
        }

    }

    public void ResetCheck()
    {
        MainCanvas.gameObject.SetActive(false);
        ResetCheckCanvas.gameObject.SetActive(true);
        Reset = true;
    }

    public void AllReset()
    {
        DataManager.Instance.SaveFloat("FG", 0);
        DataManager.Instance.SaveFloat("OG", 0);
        DataManager.Instance.SaveFloat("BG", 0);
        DataManager.Instance.SaveFloat("PG", 0);
        DataManager.Instance.SaveFloat("KG", 0);
        DataManager.Instance.SaveInt("MMG", 10);
        DataManager.Instance.SaveInt("OMG", 10);
        DataManager.Instance.SaveInt("BMG", 10);
        DataManager.Instance.SaveInt("PMG", 10);
        DataManager.Instance.SaveInt("MCMG", 0);
        DataManager.Instance.SaveInt("OCMG", 0);
        DataManager.Instance.SaveInt("BCMG", 0);
        DataManager.Instance.SaveInt("PCMG", 0);
        DataManager.Instance.SaveInt("KCMG", 0);
        DataManager.Instance.SaveString("Name","");
        DataManager.Instance.ResetAllBool();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    public void MaxCountReset()
    {
        DataManager.Instance.SaveInt("MCMG", 0);
        DataManager.Instance.SaveInt("OCMG", 0);
        DataManager.Instance.SaveInt("BCMG", 0);
        DataManager.Instance.SaveInt("PCMG", 0);

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

    /*private void UpdateGaugeDisplayB()
    {
        
        float normalizedAmount = DataManager.Instance.LoadFloat("BG") / DataManager.Instance.LoadInt("BMG");

        gaugeImages[2].fillAmount = normalizedAmount;
    }

    private void UpdateGaugeDisplayP()
    {
        float normalizedAmount = DataManager.Instance.LoadFloat("PG") / DataManager.Instance.LoadInt("PMG");

        gaugeImages[3].fillAmount = normalizedAmount;
    }*/

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
        InvalidButton(); // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("FG", 0);
        gaugeImages[0].fillAmount = 0f;

        ValidButton(); // ボタンを有効化
        isMukiButtonResetting = false;
    }

    private IEnumerator ResetOmoGauge()
    {
        isOmoButtonResetting = true;
        InvalidButton(); // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("OG", 0);
        gaugeImages[1].fillAmount = 0f;

        ValidButton(); // ボタンを有効化
        isOmoButtonResetting = false;
    }

    /*private IEnumerator ResetBetaGauge()
    {
        isBetaButtonResetting = true;
        InvalidButton(); // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("BG",0);
        gaugeImages[2].fillAmount = 0f;

        ValidButton(); // ボタンを有効化
        isBetaButtonResetting = false;
    }

    private IEnumerator ResetPataGauge()
    {
        isPataButtonResetting = true;
        InvalidButton(); // ボタンを無効化

        yield return new WaitForSeconds(2f);

        DataManager.Instance.SaveFloat("PG",0);
        gaugeImages[3].fillAmount = 0f;

        ValidButton(); // ボタンを有効化
        isPataButtonResetting = false;
    }*/

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

    private void InvalidButton()
    {
        invalidButton[0].interactable = false;
        invalidButton[1].interactable = false;
        invalidButton[2].interactable = false;
        invalidButton[3].interactable = false;
        invalidButton[4].interactable = false;
        invalidButton[5].interactable = false;
        invalidButton[6].interactable = false;
        invalidButton[7].interactable = false;
    }

    private void ValidButton()
    {
        invalidButton[0].interactable = true;
        invalidButton[1].interactable = true;
        invalidButton[2].interactable = true;
        invalidButton[3].interactable = true;
        invalidButton[4].interactable = true;
        invalidButton[5].interactable = true;
        invalidButton[6].interactable = true;
        invalidButton[7].interactable = true;
    }
}
