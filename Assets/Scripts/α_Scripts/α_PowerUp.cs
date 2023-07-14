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
}
