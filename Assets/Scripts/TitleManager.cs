using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public bool Home;

    private void Start()
    {
        Home = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Home)
        {
            SceneManager.LoadScene(1);
            DataManager.Instance.SaveBool("Home",false);
        }
    }

    public void ChangeHome()
    {
        Home = true;
    }
}
