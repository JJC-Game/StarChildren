using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public GameObject StageSelectCanvas;
    public GameObject MainCanvas;

    public bool StageSelectChange;

    [Header("ステージ選択Button")]
    [SerializeField] GameObject[] StageSelectButton;

    private int currentButtonIndex = 0; // 現在のボタンのインデックス

    void Start()
    {
        StageSelectCanvas.gameObject.SetActive(false);
        StageSelectChange = false;
        StageSelectButton[1].gameObject.SetActive(false);
        StageSelectButton[2].gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void StageSelect()
    {
        if (StageSelectChange == false)
        {
            StageSelectCanvas.gameObject.SetActive(true);
            MainCanvas.gameObject.SetActive(false);
            StageSelectChange = true;
        }
        else
        {
            StageSelectCanvas.gameObject.SetActive(false);
            MainCanvas.gameObject.SetActive(true);
            StageSelectChange = false;
        }
    }

    public void RightArrow()
    {
        ChangeButton(true);
    }

    public void LeftArrow()
    {
        ChangeButton(false);
    }

    private void ChangeButton(bool next)
    {
        StageSelectButton[currentButtonIndex].SetActive(false); // 現在のボタンを非表示にする

        if (next)
        {
            currentButtonIndex = (currentButtonIndex + 1) % StageSelectButton.Length; // 次のボタンのインデックスを計算
        }
        else
        {
            currentButtonIndex = (currentButtonIndex - 1 + StageSelectButton.Length) % StageSelectButton.Length; // 前のボタンのインデックスを計算
        }

        StageSelectButton[currentButtonIndex].SetActive(true); // 新しいボタンを表示する
    }

}
