using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public TextMeshProUGUI displayText; // 表示するテキストUIオブジェクト
    public GameObject MainGameCanvas;
    public GameObject Back;

    private TouchScreenKeyboard keyboard; // キーボードのインスタンス
    public string inputText;
    public int Limit = 8; // 文字制限

    void Start()
    {
        inputText = DataManager.Instance.LoadString("Name");
        UpdateDisplay();
        Back.gameObject.SetActive(false);
    }

    void Update()
    {
        // キーボードが非アクティブになった場合、入力を受け取る
        if (keyboard != null && !keyboard.active)
        {
            // 入力されたテキストを取得して制限を適用し、UIに表示
            string inputText = keyboard.text;
            if (inputText.Length > Limit)
            {
                inputText = inputText.Substring(0, Limit);
            }

            DataManager.Instance.SaveString("Name", inputText);
            UpdateDisplay();

            // キーボードを解放
            keyboard = null;
        }
    }

    public void NameChange()
    {
        MainGameCanvas.gameObject.SetActive(false);
        Back.gameObject.SetActive(true);
        // キーボードが表示されていない場合のみ表示する
        if (keyboard == null || !keyboard.active)
        {
            // キーボードを表示し、入力を待つ
            keyboard = TouchScreenKeyboard.Open(DataManager.Instance.LoadString("Name"), TouchScreenKeyboardType.Default);
        }
    }

    void UpdateDisplay()
    {
        // テキストUIオブジェクトにinputTextの内容を表示する
        displayText.text = inputText;
    }

}