/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class α_PowerUp : MonoBehaviour
{
    public List<Button> buttons; //押せるボタンのリスト
    public Image gaugeImage;
    public float increaseAmount = 0.1f; // ゲージの増加量
    public float maxAmount = 1.0f; // ゲージの最大値
    public GameObject mukimukiPrefab; // 追加されるキャラクターのPrefab
    public Transform mukimukiParent; // キャラクターを配置する親オブジェクトのTransform
    public Sprite newSprite; // ゲージがMAXになったときに表示する新しいImageのSprite

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
        if (!isMaxed)
        {
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
                    ShowSprite();
                }
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



    void ShowSprite()
    {
        // ImageのSpriteを新しいSpriteに変更する
        GameObject newCharacter = Instantiate(mukimukiPrefab, mukimukiParent);
        SpriteRenderer spriteRenderer = newCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class α_PowerUp : MonoBehaviour
{
    public List<Button> buttons; // 押せるボタンのリスト
    public List<Image> gaugeImages; // ゲージのImageリスト
    public List<Sprite> newSprites; // ゲージごとの追加されるSpriteリスト
    public float increaseAmount = 0.1f; // ゲージの増加量
    public float maxAmount = 1.0f; // ゲージの最大値
    public GameObject characterPrefab; // 追加されるキャラクターのPrefab
    public Transform characterParent; // キャラクターを配置する親オブジェクトのTransform

    private List<float> currentAmounts; // 各ゲージの現在の値
    private List<bool> isMaxed; // 各ゲージが最大値に達したかどうか

    private void Start()
    {
        currentAmounts = new List<float>(gaugeImages.Count);
        isMaxed = new List<bool>(gaugeImages.Count);

        // 各ゲージの初期化
        for (int i = 0; i < gaugeImages.Count; i++)
        {
            currentAmounts.Add(0.0f);
            isMaxed.Add(false);
        }

        // 各ボタンに対してイベントリスナーを追加
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // ボタンのインデックスをキャプチャする必要があるため
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (!isMaxed[buttonIndex])
        {
            // ゲージの値を増加させる
            currentAmounts[buttonIndex] += increaseAmount;
            currentAmounts[buttonIndex] = Mathf.Clamp(currentAmounts[buttonIndex], 0.0f, maxAmount);

            // ゲージの表示を更新する
            UpdateGauge(buttonIndex);

            // ゲージが最大値に達したかどうかをチェック
            if (currentAmounts[buttonIndex] >= maxAmount)
            {
                isMaxed[buttonIndex] = true;
                // ゲージが最大値になったらSpriteを表示する
                ShowSprite(buttonIndex);
            }
        }
    }

    private void UpdateGauge(int gaugeIndex)
    {
        // ゲージの表示を更新する
        float fillAmount = currentAmounts[gaugeIndex] / maxAmount;
        gaugeImages[gaugeIndex].fillAmount = fillAmount;
    }

    private void ShowSprite(int gaugeIndex)
    {
        // 新しいSpriteを持つキャラクターを追加する
        GameObject newCharacter = Instantiate(characterPrefab, characterParent);
        SpriteRenderer spriteRenderer = newCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprites[gaugeIndex];
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class α_PowerUp : MonoBehaviour
{
    public List<Button> buttons; // 押せるボタンのリスト
    public List<Image> gaugeImages; // ゲージのImageリスト
    public List<Sprite> newSprites; // ゲージごとの追加されるSpriteリスト
    public float increaseAmount = 0.1f; // ゲージの増加量
    public float maxAmount = 1.0f; // ゲージの最大値
    public GameObject characterPrefab; // 追加されるキャラクターのPrefab
    public Transform characterParent; // キャラクターを配置する親オブジェクトのTransform

    private List<float> currentAmounts; // 各ゲージの現在の値
    private List<bool> isMaxed; // 各ゲージが最大値に達したかどうか

    private void Start()
    {
        currentAmounts = new List<float>(gaugeImages.Count);
        isMaxed = new List<bool>(gaugeImages.Count);

        // 各ゲージの初期化
        for (int i = 0; i < gaugeImages.Count; i++)
        {
            currentAmounts.Add(0.0f);
            isMaxed.Add(false);
        }

        // 各ボタンに対してイベントリスナーを追加
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // ボタンのインデックスをキャプチャする必要があるため
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (!isMaxed[buttonIndex])
        {
            // ゲージの値を増加させる
            currentAmounts[buttonIndex] += increaseAmount;
            currentAmounts[buttonIndex] = Mathf.Clamp(currentAmounts[buttonIndex], 0.0f, maxAmount);

            // ゲージの表示を更新する
            UpdateGauge(buttonIndex);

            // ゲージが最大値に達したかどうかをチェック
            if (currentAmounts[buttonIndex] >= maxAmount)
            {
                isMaxed[buttonIndex] = true;
                // ゲージが最大値になったらSpriteを表示する
                ShowSprite(buttonIndex);
            }
        }
    }

    private void UpdateGauge(int gaugeIndex)
    {
        // ゲージの表示を更新する
        float fillAmount = currentAmounts[gaugeIndex] / maxAmount;
        gaugeImages[gaugeIndex].fillAmount = fillAmount;
    }

    private void ShowSprite(int gaugeIndex)
    {
        // 新しいSpriteを持つキャラクターを追加する
        GameObject newCharacter = Instantiate(characterPrefab, characterParent);
        SpriteRenderer spriteRenderer = newCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprites[gaugeIndex];
    }

    public void ResetGauge()
    {
        // ゲージをリセットする
        for (int i = 0; i < currentAmounts.Count; i++)
        {
            currentAmounts[i] = 0.0f;
            isMaxed[i] = false;
            gaugeImages[i].fillAmount = 0.0f;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class α_PowerUp : MonoBehaviour
{
    public List<Button> buttons; // 押せるボタンのリスト
    public List<Image> gaugeImages; // ゲージのImageリスト
    public List<Sprite> newSprites; // ゲージごとの追加されるSpriteリスト
    public List<float> maxAmounts; // 各ゲージの最大値リスト
    public GameObject characterPrefab; // 追加されるキャラクターのPrefab
    public Transform characterParent; // キャラクターを配置する親オブジェクトのTransform

    private List<float> currentAmounts; // 各ゲージの現在の値
    private List<bool> isMaxed; // 各ゲージが最大値に達したかどうか

    private void Start()
    {
        currentAmounts = new List<float>(gaugeImages.Count);
        isMaxed = new List<bool>(gaugeImages.Count);

        // 各ゲージの初期化
        for (int i = 0; i < gaugeImages.Count; i++)
        {
            currentAmounts.Add(0.0f);
            isMaxed.Add(false);
        }

        // 各ボタンに対してイベントリスナーを追加
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // ボタンのインデックスをキャプチャする必要があるため
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (!isMaxed[buttonIndex])
        {
            // ゲージの値を増加させる
            currentAmounts[buttonIndex] += maxAmounts[buttonIndex] / 10.0f; // ゲージの最大値の10%ずつ増加
            currentAmounts[buttonIndex] = Mathf.Clamp(currentAmounts[buttonIndex], 0.0f, maxAmounts[buttonIndex]);

            // ゲージの表示を更新する
            UpdateGauge(buttonIndex);

            // ゲージが最大値に達したかどうかをチェック
            if (currentAmounts[buttonIndex] >= maxAmounts[buttonIndex])
            {
                isMaxed[buttonIndex] = true;

                // ゲージが最大値になったらSpriteを表示する
                ShowSprite(buttonIndex);

                // ゲージを初期位置にリセットする
                ResetGauge(buttonIndex);
            }
        }
    }

    private void UpdateGauge(int gaugeIndex)
    {
        // ゲージの表示を更新する
        float fillAmount = currentAmounts[gaugeIndex] / maxAmounts[gaugeIndex];
        gaugeImages[gaugeIndex].fillAmount = fillAmount;
    }

    private void ShowSprite(int gaugeIndex)
    {
        // 新しいSpriteを持つキャラクターを追加する
        GameObject newCharacter = Instantiate(characterPrefab, characterParent);
        SpriteRenderer spriteRenderer = newCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprites[gaugeIndex];
    }

    public void ResetGauge(int gaugeIndex)
    {
        // ゲージをリセットする
        currentAmounts[gaugeIndex] = 0.0f;
        isMaxed[gaugeIndex] = false;
        gaugeImages[gaugeIndex].fillAmount = 0.0f;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class α_PowerUp : MonoBehaviour
{
    public List<Button> buttons; // 押せるボタンのリスト
    public List<Image> gaugeImages; // ゲージのImageリスト
    public List<Sprite> newSprites; // ゲージごとの追加されるSpriteリスト
    public List<float> maxAmounts; // 各ゲージの最大値リスト
    public GameObject characterPrefab; // 追加されるキャラクターのPrefab
    public Transform characterParent; // キャラクターを配置する親オブジェクトのTransform

    private List<float> currentAmounts; // 各ゲージの現在の値
    private List<bool> isMaxed; // 各ゲージが最大値に達したかどうか

    private void Start()
    {
        currentAmounts = new List<float>(gaugeImages.Count);
        isMaxed = new List<bool>(gaugeImages.Count);

        // 各ゲージの初期化
        for (int i = 0; i < gaugeImages.Count; i++)
        {
            currentAmounts.Add(0.0f);
            isMaxed.Add(false);
        }

        // 各ボタンに対してイベントリスナーを追加
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // ボタンのインデックスをキャプチャする必要があるため
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (!isMaxed[buttonIndex])
        {
            // ゲージの値を増加させる
            currentAmounts[buttonIndex] += maxAmounts[buttonIndex] / 10.0f; // ゲージの最大値の10%ずつ増加
            currentAmounts[buttonIndex] = Mathf.Clamp(currentAmounts[buttonIndex], 0.0f, maxAmounts[buttonIndex]);

            // ゲージの表示を更新する
            UpdateGauge(buttonIndex);

            // ゲージが最大値に達したかどうかをチェック
            if (currentAmounts[buttonIndex] >= maxAmounts[buttonIndex])
            {
                isMaxed[buttonIndex] = true;
                // ゲージが最大値になったらSpriteを表示する
                ShowSprite(buttonIndex);

                // ゲージを初期位置にリセットする
                ResetGauge(buttonIndex);
            }
        }
    }

    private void UpdateGauge(int gaugeIndex)
    {
        // ゲージの表示を更新する
        float fillAmount = currentAmounts[gaugeIndex] / maxAmounts[gaugeIndex];
        gaugeImages[gaugeIndex].fillAmount = fillAmount;
    }

    private void ShowSprite(int gaugeIndex)
    {
        // 新しいSpriteを持つキャラクターを追加する
        GameObject newCharacter = Instantiate(characterPrefab, characterParent);
        SpriteRenderer spriteRenderer = newCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprites[gaugeIndex];
    }

    private void ResetGauge(int gaugeIndex)
    {
        // ゲージを初期位置にリセットする
        currentAmounts[gaugeIndex] = 0.0f;
        isMaxed[gaugeIndex] = false;
        gaugeImages[gaugeIndex].fillAmount = 0.0f;
    }

    public void ResetAllGauges()
    {
        // すべてのゲージをリセットする
        for (int i = 0; i < gaugeImages.Count; i++)
        {
            ResetGauge(i);
        }
    }
}*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class α_PowerUp : MonoBehaviour
{
    [System.Serializable]
    public class Gauge
    {
        public Image gaugeImage; // ゲージのImage
        public Sprite newSprite; // ゲージがMAXになったときに表示されるSprite
        public float maxAmount; // ゲージの最大値

        [HideInInspector]
        public float currentAmount; // ゲージの現在の値
        [HideInInspector]
        public bool isMaxed; // ゲージが最大値に達したかどうか
    }

    public List<Button> buttons; // 押せるボタンのリスト
    public List<Gauge> gauges; // ゲージのリスト
    public GameObject characterPrefab; // 追加されるキャラクターのPrefab
    public Transform characterParent; // キャラクターを配置する親オブジェクトのTransform

    private void Start()
    {
        // 各ゲージの初期化
        foreach (var gauge in gauges)
        {
            gauge.currentAmount = 0.0f;
            gauge.isMaxed = false;
        }

        // 各ボタンに対してイベントリスナーを追加
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // ボタンのインデックスをキャプチャする必要があるため
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    private void OnButtonClick(int buttonIndex)
    {
        Gauge gauge = gauges[buttonIndex];

        if (!gauge.isMaxed)
        {
            // ゲージの値を増加させる
            gauge.currentAmount += gauge.maxAmount / 10.0f; // ゲージの最大値の10%ずつ増加
            gauge.currentAmount = Mathf.Clamp(gauge.currentAmount, 0.0f, gauge.maxAmount);

            // ゲージの表示を更新する
            UpdateGauge(gauge);

            // ゲージが最大値に達したかどうかをチェック
            if (gauge.currentAmount >= gauge.maxAmount)
            {
                gauge.isMaxed = true;
                // ゲージが最大値になったらSpriteを表示する
                ShowSprite(gauge);

                // ゲージを初期位置にリセットする
                ResetGauge(gauge);
            }
        }
    }

    private void UpdateGauge(Gauge gauge)
    {
        // ゲージの表示を更新する
        float fillAmount = gauge.currentAmount / gauge.maxAmount;
        gauge.gaugeImage.fillAmount = fillAmount;
    }

    private void ShowSprite(Gauge gauge)
    {
        // 新しいSpriteを持つキャラクターを追加する
        GameObject newCharacter = Instantiate(characterPrefab, characterParent);
        SpriteRenderer spriteRenderer = newCharacter.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = gauge.newSprite;
    }

    private void ResetGauge(Gauge gauge)
    {
        // ゲージを初期位置にリセットする
        gauge.currentAmount = 0.0f;
        gauge.isMaxed = false;
        gauge.gaugeImage.fillAmount = 0.0f;
    }

    public void ResetAllGauges()
    {
        // すべてのゲージをリセットする
        foreach (var gauge in gauges)
        {
            ResetGauge(gauge);
        }
    }
}


