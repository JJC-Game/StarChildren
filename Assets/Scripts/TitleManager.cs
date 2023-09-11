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
        if (Input.GetMouseButtonDown(0) && Home)
        {
            SoundManager.Instance.PlaySE_Sys(6);
            FadeManager.Instance.LoadScene("MHomeScene", 1);
            //SceneManager.LoadScene(1);
            DataManager.Instance.SaveBool("Home",false);
        }
    }

    public void ChangeHome()
    {
        Home = true;
    }
}
