using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TextMeshProUGUI Muki;
    public TextMeshProUGUI Omo;
    public TextMeshProUGUI Beta;
    public TextMeshProUGUI BetaMin;
    public TextMeshProUGUI Pata;
    private bool BetaOn;
    private bool PataOn;

    public void Update()
    {
        string convertedString1 = DataManager.Instance.LoadInt("MukiCount").ToString();
        Muki.text = convertedString1;

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

    public void DebugBetaCount()
    {
        if(BetaOn == true)
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
        if (PataOn == true)
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
