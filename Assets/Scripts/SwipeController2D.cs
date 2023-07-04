using UnityEngine;

public class SwipeController2D : MonoBehaviour
{
    // �X���C�v�̊��m����
    public float swipeDistanceThreshold = 50f;

    // �I�u�W�F�N�g�̈ړ����x
    public float speed = 5f;

    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;
    private Rigidbody2D rb;
    private bool isOnFloor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeEndPosition = Input.mousePosition;
            DetectSwipeDirection();
        }
    }

    private void DetectSwipeDirection()
    {
        Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;
        float swipeDistance = swipeDirection.magnitude;

        if (swipeDistance >= swipeDistanceThreshold && isOnFloor)
        {
            swipeDirection.Normalize();

            // �X���C�v�̑傫���ɉ����ăX�s�[�h���v�Z����
            float calculatedSpeed = swipeDistance / swipeDistanceThreshold * speed;

            Vector2 oppositeDirection = -swipeDirection;

            // �I�u�W�F�N�g���t�����Ɉړ�������
            rb.velocity = oppositeDirection * calculatedSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���ɐڐG�����ꍇ�AisOnFloor��true�ɂ���
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �����痣�ꂽ�ꍇ�AisOnFloor��false�ɂ���
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
        }
    }
}
