using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public bool T1;
    public bool T2;
    public bool T3;
    public bool T4;
    public bool T5;
    public bool T6;
    public bool T7;
    public bool T8;
    public bool T9;

    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Canvas3;
    public GameObject Canvas4;
    public GameObject Canvas5;
    public GameObject Canvas6;
    public GameObject Canvas7;
    public GameObject Canvas8;
    public GameObject Canvas9;

    void Start()
    {
        T1 = true;
        T2 = false;
        T3 = false;
        T4 = false;
        T5 = false;
        T6 = false;
        T7= false;
        T8 = false;
        T9 = false;
        Canvas1.gameObject.SetActive(true);
        Canvas2.gameObject.SetActive(false);
        Canvas3.gameObject.SetActive(false);
        Canvas4.gameObject.SetActive(false);
        Canvas5.gameObject.SetActive(false);
        Canvas6.gameObject.SetActive(false);
        Canvas7.gameObject.SetActive(false);
        Canvas8.gameObject.SetActive(false);
        Canvas9.gameObject.SetActive(false);

    }


    void Update()
    {
        if(T1 && T2 == false && Input.GetMouseButtonDown(0))
        {
            Canvas1.gameObject.SetActive(false);
            Canvas2.gameObject.SetActive(true);
            T1 = false;
            T2 = true;
        }
        else if(T2 && T3 == false && Input.GetMouseButtonDown(0))
        {
            Canvas2.gameObject.SetActive(false);
            Canvas3.gameObject.SetActive(true);
            T2 = false;
            T3 = true;
        }
        else if (T3 && T4 == false && Input.GetMouseButtonDown(0))
        {
            Canvas3.gameObject.SetActive(false);
            Canvas4.gameObject.SetActive(true);
            T3 = false;
            T4 = true;
        }
        else if (T4 && T5 == false && Input.GetMouseButtonDown(0))
        {
            Canvas4.gameObject.SetActive(false);
            Canvas5.gameObject.SetActive(true);
            T4 = false;
            T5 = true;
        }
        else if (T5 && T6 == false && Input.GetMouseButtonDown(0))
        {
            Canvas5.gameObject.SetActive(false);
            Canvas6.gameObject.SetActive(true);
            T5 = false;
            T6 = true;
        }
        else if (T6 && T7 == false && Input.GetMouseButtonDown(0))
        {
            Canvas6.gameObject.SetActive(false);
            Canvas7.gameObject.SetActive(true);
            T6 = false;
            T7 = true;
        }
        else if (T7 && T8 == false && Input.GetMouseButtonDown(0))
        {
            Canvas7.gameObject.SetActive(false);
            Canvas8.gameObject.SetActive(true);
            T7 = false;
            T8 = true;
        }
        else if (T8 && T9 == false && Input.GetMouseButtonDown(0))
        {
            Canvas8.gameObject.SetActive(false);
            Canvas9.gameObject.SetActive(true);
            T8 = false;
            T9 = true;
        }
        else if (T9 && Input.GetMouseButtonDown(0))
        {
            T9 = false;
            SoundManager.Instance.PlaySE_Sys(6);
            FadeManager.Instance.LoadScene("MHomeScene", 1);
        }
    }
}
