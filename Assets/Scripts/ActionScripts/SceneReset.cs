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

    public void ChangeScene0()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ChangeSceneHome()
    {
        DataManager.Instance.SaveInt("MeraLimit", DataManager.Instance.LoadInt("MukiCount") + DataManager.Instance.LoadInt("MeraLimit"));
        DataManager.Instance.SaveInt("OmoLimit", DataManager.Instance.LoadInt("OmoCount") + DataManager.Instance.LoadInt("OmoLimit"));
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ChangeScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeSceneTraining()
    {
        DataManager.Instance.SaveInt("MeraLimit", DataManager.Instance.LoadInt("MukiCount") + DataManager.Instance.LoadInt("MeraLimit"));
        DataManager.Instance.SaveInt("OmoLimit", DataManager.Instance.LoadInt("OmoCount") + DataManager.Instance.LoadInt("OmoLimit"));
        SceneManager.LoadScene(2);
    }

    public void ChangeScene3()
    {
        SceneManager.LoadScene(3);
    }
    public void ChangeScene4()
    {
        SceneManager.LoadScene(4);
    }
    public void ChangeScene5()
    {
        SceneManager.LoadScene(5);
    }

}
