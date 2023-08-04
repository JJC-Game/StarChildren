using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TextMeshProUGUI Muki;
    public TextMeshProUGUI Mukibool;
    public TextMeshProUGUI Omo;
    public TextMeshProUGUI Omobool;
    public TextMeshProUGUI Beta;
    public TextMeshProUGUI BetaMin;
    public TextMeshProUGUI Pata;
    private bool MukiOn;
    private bool OmoOn;
    private bool BetaOn;
    private bool PataOn;

    public void Update()
    {
        if (MukiOn == false)
        {
            string muki = "true";
            Mukibool.text = muki;
        }
        else
        {
            string muki = "false";
            Mukibool.text = muki;
        }

        string convertedString1 = DataManager.Instance.LoadInt("MukiCount").ToString();
        Muki.text = convertedString1;

        if (OmoOn == false)
        {
            string omo = "true";
            Omobool.text = omo;
        }
        else
        {
            string omo = "false";
            Omobool.text = omo;
        }

        string convertedString2 = DataManager.Instance.LoadInt("OmoCount").ToString();
        Omo.text = convertedString2;

        if (BetaOn == false)
        {
            string beta = "true";
            Beta.text = beta;
        }
        else
        {
            string beta = "false";
            Beta.text = beta;
        }

        string convertedString3 = DataManager.Instance.LoadFloat("BetaMin").ToString();
        BetaMin.text = convertedString3;

        string convertedString4 = DataManager.Instance.LoadInt("PataCount").ToString();
        Pata.text = convertedString4;

    }

    public void DebugMukiBool()
    {
        if (MukiOn)
        {
            DataManager.Instance.SaveBool("Muki", true);
            MukiOn = false;
        }
        else
        {
            DataManager.Instance.SaveBool("Muki", false);
            MukiOn = true;
        }
    }

    public void DebugOmoBool()
    {
        if (OmoOn)
        {
            DataManager.Instance.SaveBool("Omo", true);
            OmoOn = false;
        }
        else
        {
            DataManager.Instance.SaveBool("Omo", false);
            OmoOn = true;
        }
    }

    public void DebugBetaCount()
    {
        if(BetaOn)
        {
            DataManager.Instance.SaveBool("Beta", true);
            DataManager.Instance.SaveFloat("BetaMin", 0.5f);
            BetaOn = false;
        }
        else
        {
            DataManager.Instance.SaveBool("Beta", false);
            DataManager.Instance.SaveFloat("BetaMin", 0f);
            BetaOn = true;
        }
    }

    public void DebugPataCount()
    {
        if (PataOn)
        {
            DataManager.Instance.SaveInt("PataCount", 1);
            PataOn = false;
        }
        else
        {
            DataManager.Instance.SaveInt("PataCount", 0);
            PataOn = true;
        }

    }

}
