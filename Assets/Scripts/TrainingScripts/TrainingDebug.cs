﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingDebug : MonoBehaviour
{
    // 各アイテム使用可能数
    public TextMeshProUGUI ul1;
    public TextMeshProUGUI ul2;
    public TextMeshProUGUI ul3;
    public TextMeshProUGUI ul4;

    // 各ゲージがMAXになった回数
    public TextMeshProUGUI mc0;
    public TextMeshProUGUI mc1;
    public TextMeshProUGUI mc2;
    public TextMeshProUGUI mc3;
    public TextMeshProUGUI mc4;

    // キャラクターが変化したかどうか
    public TextMeshProUGUI evo;

    public void Update()
    {
        ul1.text = DataManager.Instance.LoadInt("MukiCount").ToString();
        ul2.text = DataManager.Instance.LoadInt("OmoCount").ToString();
        ul3.text = DataManager.Instance.LoadInt("BetaCount").ToString();
        ul4.text = DataManager.Instance.LoadInt("PataCount").ToString();

        mc0.text = DataManager.Instance.LoadInt("MCMG").ToString();
        mc1.text = DataManager.Instance.LoadInt("OCMG").ToString();
        mc2.text = DataManager.Instance.LoadInt("BCMG").ToString();
        mc3.text = DataManager.Instance.LoadInt("PCMG").ToString();
        mc4.text = DataManager.Instance.LoadInt("KCMG").ToString();

        evo.text = DataManager.Instance.LoadBool("E1").ToString();
    }

    // 各ボタンを押したら使用可能数を増やせる
    public void UseLimit1()
    {
        if (DataManager.Instance.LoadInt("MukiCount") != 999)
        {
            DataManager.Instance.SaveInt("MukiCount", 999);
            ul1.text = "999";
        }
        else if (DataManager.Instance.LoadInt("MukiCount") != 0)
        {
            DataManager.Instance.SaveInt("MukiCount", 0);
            ul1.text = "0";
        }

    }

    public void UseLimit2()
    {
        if (DataManager.Instance.LoadInt("OmoCount") != 999)
        {
            DataManager.Instance.SaveInt("OmoCount", 999);
            ul2.text = "999";
        }
        else if (DataManager.Instance.LoadInt("OmoCount") != 0)
        {
            DataManager.Instance.SaveInt("OmoCount", 0);
            ul2.text = "0";
        }
    }

    public void UseLimit3()
    {
        if (DataManager.Instance.LoadInt("BetaCount") != 999)
        {
            DataManager.Instance.SaveInt("BetaCount", 999);
            ul3.text = "999";
        }
        else if (DataManager.Instance.LoadInt("BetaCount") != 0)
        {
            DataManager.Instance.SaveInt("BetaCount", 0);
            ul3.text = "0";
        }
    }

    public void UseLimit4()
    {
        if (DataManager.Instance.LoadInt("PataCount") != 999)
        {
            DataManager.Instance.SaveInt("PataCount", 999);
            ul4.text = "999";
        }
        else if (DataManager.Instance.LoadInt("PataCount") != 0)
        {
            DataManager.Instance.SaveInt("PataCount", 0);
            ul4.text = "0";
        }
    }

    public void MaxCount0()
    {
        if (DataManager.Instance.LoadInt("MCMG") != 0)
        {
            DataManager.Instance.SaveInt("MCMG", 0);
        }
    }

    public void MaxCount1()
    {
        if (DataManager.Instance.LoadInt("OCMG") != 0)
        {
            DataManager.Instance.SaveInt("OCMG", 0);
        }
    }

    public void MaxCount2()
    {
        if (DataManager.Instance.LoadInt("BCMG") != 0)
        {
            DataManager.Instance.SaveInt("BCMG", 0);
        }
    }

    public void MaxCount3()
    {
        if (DataManager.Instance.LoadInt("PCMG") != 0)
        {
            DataManager.Instance.SaveInt("PCMG", 0);
        }
    }

    public void MaxCount4()
    {
        if (DataManager.Instance.LoadInt("KCMG") != 0)
        {
            DataManager.Instance.SaveInt("KCMG", 0);
        }
    }

    public void EvoReset()
    {
        DataManager.Instance.SaveBool("E1", false);
        DataManager.Instance.SaveBool("E1F", false);
        DataManager.Instance.SaveBool("E1O", false);
    }

}