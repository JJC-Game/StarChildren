using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : Singleton<EffectManager>
{
    // 2Dパーティクルエフェクトのプレハブ配列
    public GameObject[] particleEffectPrefabs;

    public float deleteTime = 1.0f;

    // エフェクトを再生するメソッド
    public void PlayEffect(int index, Transform targetTransform)
    {
        if (index >= 0 && index < particleEffectPrefabs.Length)
        {
            GameObject effectPrefab = particleEffectPrefabs[index];

            if (effectPrefab != null && targetTransform != null)
            {
                // ターゲットの位置にエフェクトを再生
                GameObject effectInstance = Instantiate(effectPrefab, targetTransform.position, Quaternion.identity);

                // エフェクトの再生が終わったら一定時間後に破棄
                Destroy(effectInstance, deleteTime);
            }
        }
    }

    // タグが付いているオブジェクトにエフェクトを再生するメソッド
    public void PlayEffectOnTaggedObjects(int effectIndex, string targetTag)
    {
        // タグが付いているオブジェクトを検索
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        // タグが付いているオブジェクトにエフェクトを再生
        foreach (GameObject taggedObject in taggedObjects)
        {
            PlayEffect(effectIndex, taggedObject.transform);
        }
    }
}