using System.Collections;
using System.IO;
using UnityEngine;

public class TweetController : MonoBehaviour
{
    string imgPath;                         //画像の保存先
    const string fileName = "/image.png";    //保存するデータ名

    //ボタンから呼び出し
    public void Tweet()
    {
        Debug.Log("ボタンを押したよ");
        StartCoroutine(ShareCoroutine());
    }

    public IEnumerator ShareCoroutine()
    {
        ScreenCapture.CaptureScreenshot(fileName);
        imgPath = Path.Combine(Application.persistentDataPath + fileName);

        yield return new WaitForSeconds(1); //一秒待つ（撮影が完了するまでラグがあるので）

        new NativeShare().AddFile(imgPath)
        .SetSubject("").SetText("#Test #テスト").SetUrl("")
        .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
        .Share();
    }
}