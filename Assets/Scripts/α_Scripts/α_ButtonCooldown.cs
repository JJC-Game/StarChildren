using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class α_ButtonCooldown : MonoBehaviour
{
    public Button button;
    private bool canPressButton = true;
    private WaitForSeconds cooldownDuration = new WaitForSeconds(60f);
    private Coroutine cooldownCoroutine;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
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
            SceneManager.LoadScene("α_ijiri");
        }
    }

    private IEnumerator StartCooldown()
    {
        canPressButton = false;
        yield return cooldownDuration;
        canPressButton = true;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
    }
}