using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public bool Home;

    private void Start()
    {

        SoundManager.Instance.PlayBGM(0);

        Home = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Home && DataManager.Instance.LoadBool("Tutorial") == true)
        {
            SoundManager.Instance.PlaySE_Sys(9);
            FadeManager.Instance.LoadScene("MHomeScene", 1);
            //SceneManager.LoadScene(1);
        }
        else if (Input.GetMouseButtonDown(0) && Home && DataManager.Instance.LoadBool("Tutorial") == false)
        {
            SoundManager.Instance.PlaySE_Sys(9);
            FadeManager.Instance.LoadScene("TutorialScene", 1);
            DataManager.Instance.SaveBool("Tutorial", true);
        }
    }

    public void ChangeHome()
    {
        Home = true;
    }
}
