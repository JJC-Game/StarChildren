using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText; // �\������TextMeshProUGUI�I�u�W�F�N�g

    private float startTime;
    private bool isTimerRunning;

    private void Start()
    {
        // �v�����J�n����
        StartTimer();
    }

    private void Update()
    {
        // �^�C�}�[�����s���̏ꍇ�A�o�ߎ��Ԃ��X�V���ĕ\������
        if (isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void StartTimer()
    {
        // �^�C�}�[���J�n���A�J�n�������L�^����
        isTimerRunning = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        // �^�C�}�[���~����
        isTimerRunning = false;
    }

    private void UpdateTimerText(float elapsedTime)
    {
        // �o�ߎ��Ԃ��e�L�X�g�I�u�W�F�N�g�ɕ\������
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        string timeString = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        timerText.text = timeString;
    }
}
