using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : Singleton<EffectManager>
{
    public GameObject[] effectPrefabs; // エフェクトのプレハブを配列で管理

    // エフェクトを再生するメソッド
    public void PlayEffect(int effectIndex, Vector3 position)
    {
        if (effectIndex < 0 || effectIndex >= effectPrefabs.Length)
        {
            Debug.LogWarning("Invalid effect index: " + effectIndex);
            return;
        }

        Instantiate(effectPrefabs[effectIndex], position, Quaternion.identity);
    }
}


