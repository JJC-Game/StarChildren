using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class α_DisableButton : MonoBehaviour
{
    public Button button;
    public float cooldownDuration = 20f;
    private Color disabledColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    private Color normalColor;
    private bool canPressButton = true;
    private Coroutine cooldownCoroutine;
    private static bool cooldownActive = false;

    private void Start()
    {
        // ボタンの通常の色を保存しておく
        normalColor = button.colors.normalColor;

        // ボタンをアクティブにする
        EnableButton();
    }

    public void EnableButton()
    {
        // ボタンをアクティブにし、通常の色に戻す
        canPressButton = true;
        button.interactable = true;
        button.image.color = normalColor;
    }

    public void DisableButtonTemporarily()
    {
        // クールダウンが既にアクティブな場合は無視する
        if (cooldownActive)
            return;

        // ボタンを非アクティブにし、灰色にする
        canPressButton = false;
        button.interactable = false;
        button.image.color = disabledColor;
        cooldownCoroutine = StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownDuration);
        cooldownActive = false;
        EnableButton();
    }

    public void GoToTargetScene()
    {
        if (canPressButton)
        {
            if (cooldownCoroutine != null)
            {
                StopCoroutine(cooldownCoroutine);
            }
            SceneManager.LoadScene("α_ijiri");
        }
    }
}