using UnityEngine;
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

    public void ChangeSceneHomeinAction()
    {
        DataManager.Instance.SaveInt("MukiCount", PlayerHitCheck.Instance.MukiCount);
        DataManager.Instance.SaveInt("OmoCount", PlayerHitCheck.Instance.OmoCount);
        DataManager.Instance.SaveInt("MeraLimit", DataManager.Instance.LoadInt("MukiCount") + DataManager.Instance.LoadInt("MeraLimit"));
        DataManager.Instance.SaveInt("OmoLimit", DataManager.Instance.LoadInt("OmoCount") + DataManager.Instance.LoadInt("OmoLimit"));
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ChangeSceneHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeSceneAction()
    {
        SceneManager.LoadScene(1);
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
            Debug.Log("a");
        }
        SceneManager.LoadScene(2);
    }

    public void ChangeSceneTraining()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeSceneEnding()
    {
        SceneManager.LoadScene(3);
    }

    public void ChangeSceneAlbum()
    {
        SceneManager.LoadScene(4);
    }

    public void ChangeSceneStageSelect()
    {
        SceneManager.LoadScene(5);
    }

    public void ChangeSceneα_stage1()
    {
        SceneManager.LoadScene(6);
        DataManager.Instance.SaveInt("Stage", 1);
    }

    public void ChangeSceneα_stage2()
    {
        SceneManager.LoadScene(7);
        DataManager.Instance.SaveInt("Stage", 2);
    }

    public void ChangeSceneα_stage3()
    {
        SceneManager.LoadScene(8);
        DataManager.Instance.SaveInt("Stage", 3);
    }

}
