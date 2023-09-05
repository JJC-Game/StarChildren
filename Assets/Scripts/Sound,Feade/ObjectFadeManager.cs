using UnityEngine;
using System.Collections;

public class ObjectFadeManager : Singleton<ObjectFadeManager>
{
    private Material fadeMaterial; // フェードに使用するマテリアル
    public string objectTag = "YourTag"; // フェードアウト対象のオブジェクトのタグ
    private GameObject[] fadeObjects; // フェードアウト対象のオブジェクトの配列

    private void Start()
    {
        // シーン内のフェードに使用するオブジェクトを見つけてマテリアルを取得
        // タグを使用して対象オブジェクトを探す
        fadeObjects = GameObject.FindGameObjectsWithTag(objectTag);

        if (fadeObjects.Length > 0)
        {
            fadeMaterial = fadeObjects[0].GetComponent<Renderer>().material; // 最初のオブジェクトを使用する例
            fadeMaterial.color = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, 0f);
        }
    }

    // フェードアウト処理
    public void FadeOutAndDestroy(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        if (fadeMaterial == null || fadeObjects.Length == 0)
        {
            yield break;
        }

        float startTime = Time.time;
        float startAlpha = fadeMaterial.color.a;
        float targetAlpha = 0f;

        while (Time.time - startTime < duration)
        {
            float elapsed = Time.time - startTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            fadeMaterial.color = new Color(fadeMaterial.color.r, fadeMaterial.color.g, fadeMaterial.color.b, alpha);
            yield return null;
        }

        // フェードアウト完了後、オブジェクトを破棄する
        foreach (GameObject obj in fadeObjects)
        {
            Destroy(obj);
        }
    }
}
