﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    public void ResetScene()
    {
        DataManager.Instance.ResetMukiCount();
        DataManager.Instance.ResetOmoCount();
        DataManager.Instance.ResetBetaCount();
        DataManager.Instance.ResetPataCount();
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ChangeSceneTitle()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MTitleScene", 1);

        //SceneManager.LoadScene(0);
    }

    public void ChangeSceneHomeinAction()
    {
        DataManager.Instance.SaveInt("MukiCount", PlayerHitCheck.Instance.MukiCount);
        DataManager.Instance.SaveInt("OmoCount", PlayerHitCheck.Instance.OmoCount);
        DataManager.Instance.SaveInt("MeraLimit", DataManager.Instance.LoadInt("MukiCount") + DataManager.Instance.LoadInt("MeraLimit"));
        DataManager.Instance.SaveInt("OmoLimit", DataManager.Instance.LoadInt("OmoCount") + DataManager.Instance.LoadInt("OmoLimit"));
        Time.timeScale = 1f;

        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MHomeScene", 1);

        //SceneManager.LoadScene(1);
    }

    public void ChangeSceneHome()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MHomeScene", 1);

        //SceneManager.LoadScene(1);
    }

    public void ChangeSceneTraininginAction()
    {
        DataManager.Instance.SaveInt("MukiCount", PlayerHitCheck.Instance.MukiCount);
        DataManager.Instance.SaveInt("OmoCount", PlayerHitCheck.Instance.OmoCount);
        DataManager.Instance.SaveInt("MeraLimit", DataManager.Instance.LoadInt("MukiCount") + DataManager.Instance.LoadInt("MeraLimit"));
        DataManager.Instance.SaveInt("OmoLimit", DataManager.Instance.LoadInt("OmoCount") + DataManager.Instance.LoadInt("OmoLimit"));
        if (DataManager.Instance.LoadBool("ClearReset"))
        {
            DataManager.Instance.DeleteAllBool();
        }
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MTrainingScene", 1);
    }

    public void ChangeSceneTraining()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MTrainingScene", 1);
        //SceneManager.LoadScene(2);
    }

    public void ChangeSceneEnding()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MEndingScene", 1);
        //SceneManager.LoadScene(3);
    }

    public void ChangeSceneAlbum()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("MAlbumScene", 1);
        //SceneManager.LoadScene(4);
    }

    public void ChangeScenestage1()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("M_stage1", 1);
        //SceneManager.LoadScene(5);
        DataManager.Instance.SaveInt("Stage", 1);
    }

    public void ChangeScenestage2()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("M_stage2", 1);
        //SceneManager.LoadScene(6);
        DataManager.Instance.SaveInt("Stage", 2);
    }

    public void ChangeScenestage3()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("M_stage3", 1);
        //SceneManager.LoadScene(7);
        DataManager.Instance.SaveInt("Stage", 3);
    }

    public void ChangeSceneTutorial()
    {
        SoundManager.Instance.PlaySE_Sys(6);
        FadeManager.Instance.LoadScene("TutorialScene", 1);

    }

}
