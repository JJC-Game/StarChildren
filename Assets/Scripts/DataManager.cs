using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
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
        PlayerPrefs.SetInt(key, value ? 1 : 0); // 1 for true, 0 for false
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


    // 全変数のリセット
    public void ResetAllVariables()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void ResetMukiCount()
    {
        PlayerPrefs.DeleteKey("ItemCountMuki");
        PlayerPrefs.Save();
    }

    public void ResetOmoCount()
    {
        PlayerPrefs.DeleteKey("ItemCountOmo");
        PlayerPrefs.Save();
    }
    
    public void ResetBetaCount()
    {
        PlayerPrefs.DeleteKey("ItemCountBeta");
        PlayerPrefs.Save();
    }

    public void ResetPataCount()
    {
        PlayerPrefs.DeleteKey("ItemCountPata");
        PlayerPrefs.Save();
    }
    
    /*
    特定の変数をリセットする場合
    PlayerPrefs.DeleteKey("任意");

    保存されている値を呼び出し、変更して再保存する場合
    int a = GameDataManager.LoadInt("任意");
    a -= 1;
    GameDataManager.SaveInt("任意", a);
    */
}
