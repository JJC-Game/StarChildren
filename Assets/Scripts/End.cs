using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public bool Tap;
    public int clearcount;

    private void Start()
    {
        Tap = false;
    }

    private void Update()
    {
        if (Tap == true && Input.GetMouseButtonDown(0))
        {
            TapToAlbum();
        }
    }

    public void TapOn()
    {
        Tap = true;
    }

    public void TapToAlbum()
    {
        clearcount = DataManager.Instance.LoadInt("Clear");
        DataManager.Instance.SaveInt("Clear", clearcount + 1);
        DataManager.Instance.ResetGame();
        DataManager.Instance.SaveBool("Album", true);
        SceneManager.LoadScene(4);
    }
}
