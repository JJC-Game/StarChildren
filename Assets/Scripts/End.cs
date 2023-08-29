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
            TapAlbum();
        }
    }

    public void TapOn()
    {
        Tap = true;
    }

    public void TapAlbum()
    {
        clearcount = DataManager.Instance.LoadInt("Clear");
        DataManager.Instance.SaveInt("Clear", clearcount + 1);
        SceneManager.LoadScene(3);
    }
}
