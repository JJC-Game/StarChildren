using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public TextMeshProUGUI displayText; // 表示するテキストUIオブジェクト
    private string inputText; // ユーザーが入力した文字を保持する変数
    private int maxInputLength = 8; // 入力上限の文字数
    public bool Name;
    public GameObject MainGameCanvas;
    public GameObject Back;

    void Start()
    {
        inputText = DataManager.Instance.LoadString("Name");
        UpdateDisplay();
        Name = false;
        Back.gameObject.SetActive(false);
    }

    void Update()
    {
        // ユーザーの入力を受け取る
        if (Input.inputString.Length > 0 && Name)
        {
            char c = Input.inputString[0];
            // 入力された文字がバックスペースでない場合は、inputTextに追加
            if (c != '\b' && inputText.Length < maxInputLength)
            {
                inputText += c;
            }
            // 入力された文字がバックスペースの場合は、inputTextの末尾を削除
            else if (c == '\b' && inputText.Length > 0)
            {
                inputText = inputText.Substring(0, inputText.Length - 1);
            }

            UpdateDisplay();
            DataManager.Instance.SaveString("Name", inputText);
        }
    }

    void UpdateDisplay()
    {
        // テキストUIオブジェクトにinputTextの内容を表示する
        displayText.text = inputText;
    }

    public void NameChange()
    {
        if (Name == false)
        {
            Name = true;
            MainGameCanvas.gameObject.SetActive(false);
            Back.gameObject.SetActive(true);
        }
        else if (Name == true)
        {
            Name = false;
            MainGameCanvas.gameObject.SetActive(true);
            Back.gameObject.SetActive(false);
        }
    }
}