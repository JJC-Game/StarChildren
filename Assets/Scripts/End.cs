using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public bool Tap;

    private void Start()
    {
        Tap = false;
    }

    private void Update()
    {
        if (Tap == true && Input.GetMouseButtonDown(0))
        {
            TapTitle();
        }
    }

    public void TapOn()
    {
        Tap = true;
    }

    public void TapTitle()
    {
        SceneManager.LoadScene(4);
    }
}
