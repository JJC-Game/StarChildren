using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public int clearcount;
    public int Display0;
    public int Display1;
    public int Display2;
    public int Display3;
    public int Display4;
    public int Display5;
    public bool FFF;
    public bool OOO;
    public bool FFO;
    public bool OOF;
    public string Name;
    public string CName0;
    public string CName1;
    public string CName2;
    public string CName3;
    public string CName4;
    public string CName5;

    // 変数の保存int型
    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // 変数の保存float型
    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    // 変数の保存bool型（1がtrue、0がfalse）
    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0); // 1がtrue, 0がfalse
        PlayerPrefs.Save();
    }

    // 変数の保存string型
    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // 保存した変数の呼び出しint型
    public int LoadInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    // 保存した変数の呼び出しfloat型
    public float LoadFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    // 保存した変数の呼び出しbool型
    public bool LoadBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1;
    }

    // 保存した変数の呼び出しstring型
    public string LoadString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    // 全変数のリセット
    public void ResetAll()
    {
        clearcount = LoadInt("Clear");
        Display0 = LoadInt("Display0");
        Display1 = LoadInt("Display1");
        Display2 = LoadInt("Display2");
        Display3 = LoadInt("Display3");
        Display4 = LoadInt("Display4");
        Display5 = LoadInt("Display5");
        CName0 = LoadString("CName0");
        CName1 = LoadString("CName1");
        CName2 = LoadString("CName2");
        CName3 = LoadString("CName3");
        CName4 = LoadString("CName4");
        CName5 = LoadString("CName5");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SaveInt("Clear", clearcount);
        SaveInt("Display0", Display0);
        SaveInt("Display1", Display1);
        SaveInt("Display2", Display2);
        SaveInt("Display3", Display3);
        SaveInt("Display4", Display4);
        SaveInt("Display5", Display5);
        SaveString("CName0", CName0);
        SaveString("CName1", CName1);
        SaveString("CName2", CName2);
        SaveString("CName3", CName3);
        SaveString("CName4", CName4);
        SaveString("CName5", CName5);
    }

    public void ResetGame()
    {
        clearcount =  LoadInt("Clear");
        Display0 = LoadInt("Display0");
        Display1 = LoadInt("Display1");
        Display2 = LoadInt("Display2");
        Display3 = LoadInt("Display3");
        Display4 = LoadInt("Display4");
        Display5 = LoadInt("Display5");
        FFF = LoadBool("E3FFF");
        OOO = LoadBool("E3OOO");
        FFO = LoadBool("E3FFO");
        OOF = LoadBool("E3OOF");
        Name = LoadString("Name");
        CName0 = LoadString("CName0");
        CName1 = LoadString("CName1");
        CName2 = LoadString("CName2");
        CName3 = LoadString("CName3");
        CName4 = LoadString("CName4");
        CName5 = LoadString("CName5");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SaveInt("Clear", clearcount);
        SaveInt("Display0", Display0);
        SaveInt("Display1", Display1);
        SaveInt("Display2", Display2);
        SaveInt("Display3", Display3);
        SaveInt("Display4", Display4);
        SaveInt("Display5", Display5);
        SaveBool("E3FFF", FFF);
        SaveBool("E3OOO", OOO);
        SaveBool("E3FFO", FFO);
        SaveBool("E3OOF", OOF);
        SaveString("Name", Name);
        SaveString("CName0", CName0);
        SaveString("CName1", CName1);
        SaveString("CName2", CName2);
        SaveString("CName3", CName3);
        SaveString("CName4", CName4);
        SaveString("CName5", CName5);
    }

    public void ResetMukiCount()
    {
        PlayerPrefs.DeleteKey("MukiCount");
        PlayerPrefs.Save();
    }

    public void ResetOmoCount()
    {
        PlayerPrefs.DeleteKey("OmoCount");
        PlayerPrefs.Save();
    }
    
    public void ResetBetaCount()
    {
        PlayerPrefs.DeleteKey("BetaCount");
        PlayerPrefs.Save();
    }

    public void ResetPataCount()
    {
        PlayerPrefs.DeleteKey("PataCount");
        PlayerPrefs.Save();
    }

    public void ResetAllBool()
    {
        SaveBool("E1F", false);
        SaveBool("E1O", false);
        SaveBool("E2FF", false);
        SaveBool("E2OO", false);
        SaveBool("E2FO", false);
        SaveBool("E3FFF", false);
        SaveBool("E3OOO", false);
        SaveBool("E3FFO", false);
        SaveBool("E3FOO", false);
        SaveBool("Finish", false);
        SaveBool("E1", false);
        SaveBool("E2", false);
        SaveBool("E3", false);
        SaveBool("E4", false);
        SaveBool("Muki", false);
        SaveBool("Omo", false);
        SaveBool("Album", false);
    }

    //"E1F", "E1O","E2FF", "E1OO", "E2FO", "E3FFF", "E3OOO", "E3FFO", "E3FOO", "Finish", "E1", "E2", "E3", "E4", "Muki", "Omo", "Album" 

    /*
    特定の変数をリセットする場合
    PlayerPrefs.DeleteKey("任意");

    保存されている値を呼び出し、変更して再保存する場合
    int a = GameDataManager.LoadInt("任意");
    a -= 1;
    GameDataManager.SaveInt("任意", a);
    */
}
