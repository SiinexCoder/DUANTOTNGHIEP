using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển
    private Vector2 moveDirection; // Hướng di chuyển
    private Vector3 targetPosition; // Vị trí mục tiêu
    private bool isMoving = false; // Trạng thái di chuyển
    private Animator animator; // Animator của nhân vật

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator từ nhân vật
    }

    void Update()
    {
        // Nhấn chuột trái để di chuyển
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // Giữ Z ở mức 0
            moveDirection = (targetPosition - transform.position).normalized;
            isMoving = true;
        }

        // Kiểm tra nếu đã tới vị trí mục tiêu
        if (isMoving && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }

        // Xoay nhân vật
        if (isMoving)
        {
            RotatePlayer(targetPosition);
        }

        // Cập nhật Animator
        animator.SetBool("isRunning", isMoving);
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position += (Vector3)moveDirection * moveSpeed * Time.fixedDeltaTime;
        }
    }

    void RotatePlayer(Vector3 targetPosition)
    {
        float direction = targetPosition.x - transform.position.x;
        if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Quay phải
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Quay trái
        }
    }
}
