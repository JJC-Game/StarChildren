using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingDebug : MonoBehaviour
{
    public bool UL1;
    public bool UL2;
    public bool UL3;
    public bool UL4;

    public TextMeshProUGUI ul1;
    public TextMeshProUGUI ul2;
    public TextMeshProUGUI ul3;
    public TextMeshProUGUI ul4;

    public void Start()
    {
        ul1.text = DataManager.Instance.LoadInt("MukiCount").ToString();
        ul2.text = DataManager.Instance.LoadInt("OmoCount").ToString();
        ul3.text = DataManager.Instance.LoadInt("BetaCount").ToString();
        ul4.text = DataManager.Instance.LoadInt("PataCount").ToString();
    }

    public void Update()
    {
        ul1.text = DataManager.Instance.LoadInt("MukiCount").ToString();
        ul2.text = DataManager.Instance.LoadInt("OmoCount").ToString();
        ul3.text = DataManager.Instance.LoadInt("BetaCount").ToString();
        ul4.text = DataManager.Instance.LoadInt("PataCount").ToString();
    }

    public void UseLimit1()
    {
        if (UL1 == true)
        {
            DataManager.Instance.SaveInt("MukiCount", 999);
            ul1.text = "999";
        }
        else
        {
            DataManager.Instance.SaveInt("MukiCount", 0);
            ul1.text = "0";
        }

    }

    public void UseLimit2()
    {
        if (UL2 == true)
        {
            DataManager.Instance.SaveInt("OmoCount", 999);
            ul2.text = "999";
        }
        else
        {
            DataManager.Instance.SaveInt("OmoCount", 0);
            ul2.text = "0";
        }
    }

    public void UseLimit3()
    {
        if (UL3 == true)
        {
            DataManager.Instance.SaveInt("BetaCount", 999);
            ul3.text = "999";
        }
        else
        {
            DataManager.Instance.SaveInt("BetaCount", 0);
            ul3.text = "0";
        }
    }

    public void UseLimit4()
    {
        if (UL4 == true)
        {
            DataManager.Instance.SaveInt("PataCount", 999);
            ul4.text = "999";
        }
        else
        {
            DataManager.Instance.SaveInt("PataCount", 0);
            ul4.text = "0";
        }
    }

}
