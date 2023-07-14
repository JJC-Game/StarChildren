using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class α_ButtonCoolManager : MonoBehaviour
{
    public Button button;
    private bool canPressButton = true;
    private WaitForSeconds cooldownDuration = new WaitForSeconds(60f);
    private Coroutine cooldownCoroutine;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (canPressButton)
        {
            if (cooldownCoroutine != null)
            {
                StopCoroutine(cooldownCoroutine);
            }
            cooldownCoroutine = StartCoroutine(StartCooldown());
            // ボタンクリック時の処理を追加してください
        }
    }

    private IEnumerator StartCooldown()
    {
        canPressButton = false;
        yield return cooldownDuration;
        canPressButton = true;
    }
}