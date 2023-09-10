using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeManager : MonoBehaviour
{
    public GameObject StageSelectCanvas;
    public GameObject MainCanvas;
    public GameObject OptionCanvas;
    public TextMeshProUGUI Name;

    private bool StageSelectChange;
    private bool optionChange;

    [Header("ステージ選択Button")]
    [SerializeField] GameObject[] StageSelectButton;

    private int currentButtonIndex = 0; // 現在のボタンのインデックス

    public GameObject PlayerSprite; //キャラクターの見た目
    public Sprite[] EvolveSprite; //進化後のキャラクターの見た目

    void Start()
    {
        StageSelectCanvas.gameObject.SetActive(false);
        OptionCanvas.gameObject.SetActive(false);
        StageSelectChange = false;
        StageSelectButton[1].gameObject.SetActive(false);
        StageSelectButton[2].gameObject.SetActive(false);
        Name.text = DataManager.Instance.LoadString("Name");

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
        SoundManager.Instance.PlayBGM(1);
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

    public void Option()
    {
        if (optionChange == false)
        {
            OptionCanvas.gameObject.SetActive(true);
            MainCanvas.gameObject.SetActive(false);
            optionChange = true;
        }
        else
        {
            OptionCanvas.gameObject.SetActive(false);
            MainCanvas.gameObject.SetActive(true);
            optionChange = false;
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
