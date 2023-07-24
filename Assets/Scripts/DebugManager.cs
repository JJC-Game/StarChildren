using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TextMeshProUGUI Muki;
    public TextMeshProUGUI Omo;
    public TextMeshProUGUI Beta;
    public TextMeshProUGUI Pata;
    private bool BetaOn;
    private bool PataOn;

    public void Update()
    {
        string convertedString1 = DataManager.Instance.LoadInt("MukiCount").ToString();
        Muki.text = convertedString1;
        string convertedString2 = DataManager.Instance.LoadInt("OmoCount").ToString();
        Omo.text = convertedString2;
        string convertedString3 = DataManager.Instance.LoadInt("BetaCount").ToString();
        Beta.text = convertedString3;
        string convertedString4 = DataManager.Instance.LoadInt("PataCount").ToString();
        Pata.text = convertedString4;

    }

    public void DebugBetaCount()
    {
        if(BetaOn == true)
        {
            DataManager.Instance.SaveInt("BetaCount", 1);
            BetaOn = false;
        }
        else
        {
            DataManager.Instance.SaveInt("BetaCount", 0);
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
