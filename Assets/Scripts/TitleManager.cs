using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && DataManager.Instance.LoadBool("Home"))
        {
            SceneManager.LoadScene(1);
            DataManager.Instance.SaveBool("Home",false);
        }
    }
}
