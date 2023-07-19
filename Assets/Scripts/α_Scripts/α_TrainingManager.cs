using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class α_TrainingManager : MonoBehaviour
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

    private bool isButtonEnabled = true; // ボタンの有効化フラグ
    private bool isResettingGauge = false; // ゲージのリセット中フラグ
    private float resetTimer = 0f; // ゲージリセット用のタイマー

    //public Sprite[] sprites; // 生成するスプライトの配列
    //public Transform parentObject; // スプライトの親オブジェクト

    //private int currentIndex = -1; // 現在の生成スプライトのインデックス
    //private int[] remainingIndices; // 未生成のスプライトのインデックスの配列

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

        if (isResettingGauge)
        {
            // ゲージリセット中の処理
            resetTimer += Time.deltaTime;

            if (resetTimer >= 2f)
            {
                // 2秒経過後にゲージをリセット
                isResettingGauge = false;
                resetTimer = 0f;
                currentAmounts[4] = 0f;
                ResetOtherGauges();
                UpdateImage = 4;
                UpdateGaugeDisplay();

                // ボタンを再度有効化
                isButtonEnabled = true;
            }
        }
    }


    // ボタンが押された時に呼ばれる関数
    public void MukiButton()
    {
        if (isButtonEnabled)
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
            isButtonEnabled = false; // ボタンを無効化
        }
    }

    public void OmoButton()
    {
        if (isButtonEnabled)
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
            isButtonEnabled = false; // ボタンを無効化
        }
    }


    public void BetaButton()
    {
        if (isButtonEnabled)
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
            isButtonEnabled = false; // ボタンを無効化
        }
    }

    public void PataButton()
    {
        if (isButtonEnabled)
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
            isButtonEnabled = false; // ボタンを無効化
        }
    }
    

    /*public void GenerateNextSprite()
    {
        if (currentIndex >= 0 && currentIndex < transform.childCount)
        {
            Destroy(transform.GetChild(currentIndex).gameObject);
        }

        if (remainingIndices.Length == 0)
        {
            ResetGenerator();
        }

        int randomIndex = Random.Range(0, remainingIndices.Length);
        currentIndex = remainingIndices[randomIndex];
        remainingIndices[randomIndex] = remainingIndices[remainingIndices.Length - 1];
        System.Array.Resize(ref remainingIndices, remainingIndices.Length - 1);

        Sprite sprite = sprites[currentIndex];
        GameObject spriteObj = new GameObject("Sprite");
        spriteObj.transform.position = transform.position;
        spriteObj.transform.SetParent(parentObject);
        spriteObj.AddComponent<SpriteRenderer>().sprite = sprite;
    }

    private void ResetGenerator()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        currentIndex = -1;
        remainingIndices = new int[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            remainingIndices[i] = i;
        }
    }*/

    public void KiraGauge()
    {
        // ゲージ量が最大値を超えた場合、ゲージをリセットする
        if (currentAmounts[4] - 1 >= maxAmounts[4])
        {
            isButtonEnabled = false; // ボタンを無効化
            isResettingGauge = true; // ゲージリセットフラグを有効化
            currentAmounts[4] = 0f;
            ResetOtherGauges();
        }

        UpdateImage = 4;
        UpdateGaugeDisplay();
    }

    // 他のゲージをリセットする
    private void ResetOtherGauges()
    {
        for (int i = 0; i < gaugeImages.Length; i++)
        {
            if (i != UpdateImage)
            {
                currentAmounts[i] = 0f;
            }
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
}