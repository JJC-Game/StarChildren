using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject; // ���W�𔽉f������Ώۂ̃I�u�W�F�N�g

    private void Update()
    {
        // �^�[�Q�b�g�I�u�W�F�N�g��Y���W�����݂̃I�u�W�F�N�g�ɔ��f
        Vector3 newPosition = transform.position;
        newPosition.y = targetObject.position.y;
        transform.position = newPosition;
    }
}
