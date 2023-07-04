using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private bool isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �v���C���[���͈͓��ɓ������ꍇ
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            StopGame();
        }
    }

    private void StopGame()
    {
        // �Q�[���̒�~���������s����i�Ⴆ�΃^�C���X�P�[���̕ύX�Ȃǁj
        Time.timeScale = 0f;
    }
}
