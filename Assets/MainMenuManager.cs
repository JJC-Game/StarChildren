using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //追加

public class MainMenuManager : MonoBehaviour
{
    [Header("各Canvas")]
    [SerializeField] GameObject[] canvas;

    [Header("各メニューの初期カーソル対象")]
    [SerializeField] GameObject[] focusObject;

    //フォーカスが外れてしまう時の対処
    GameObject currentFocus;    //現在の選択している対象
    GameObject previousFocus;   //前フレームまで選択していた対象



    void Start()
    {
        CanvasInit();   //全てのcanvasを非表示に

        canvas[0].SetActive(true);  //タイトルのキャンバスだけtrue

        //タイトルの初期カーソル位置を設定
        EventSystem.current.SetSelectedGameObject(focusObject[0]);

        SoundManager.Instance.BGMSource.Play(1);


    }

    void Update()
    {
        FocusCheck(); //フォーカス対象のチェック
    }


    //全てのcanvasを非表示に
    void CanvasInit()
    {
        //canvasGroupの要素数をCanvasの数と同じに
        //canvasGroup = new CanvasGroup[canvas.Length];

        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }

    //メニューの遷移（各メニュー項目のイベントトリガーでキャンバスの番号を指定）
    public void Transition_Menu(int nextMenu)
    {
        //一度全てのメニューを非表示
        CanvasInit();

        //次のメニューを表示
        canvas[nextMenu].SetActive(true);

        //次のメニューの初期カーソル位置を設定
        EventSystem.current.SetSelectedGameObject(focusObject[nextMenu]);

        //フォーカス対象の更新
        //currentFocus = previousFocus = EventSystem.current.currentSelectedGameObject;
    }

    //フォーカス対象のチェック
    void FocusCheck()
    {
        //現在フォーカス中の項目を格納
        currentFocus = EventSystem.current.currentSelectedGameObject;

        //前回までの選択と変わらない場合は何もせず終了
        if (currentFocus == previousFocus) return;

        //フォーカス対象を失った場合、前フレームまで選択していた項目を選択状態に
        if (currentFocus == null)
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        //残された条件から、フォーカスしている項目が存在するのが確定
        //前フレームの対象を更新する
        previousFocus = EventSystem.current.currentSelectedGameObject;
    }

    //ゲーム終了ボタン
    public void Quit()
    {
        //UnityEditor上でプレイを終了する場合
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //ビルドした実行データでプレイを終了する場合
            Application.Quit();
#endif
    }

}