using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của Enemy
    public float waitTime = 2f; // Thời gian dừng giữa các lần di chuyển
    public Vector2 movementRange = new Vector2(5f, 5f); // Phạm vi di chuyển

    private Vector3 startPosition; // Vị trí ban đầu của Enemy
    private Vector3 targetPosition; // Vị trí mục tiêu để Enemy di chuyển tới
    private bool isMoving = false; // Kiểm tra trạng thái di chuyển
    private Vector3 lastPosition; // Lưu lại vị trí lần cuối để kiểm tra hướng

    private void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu của Enemy
        lastPosition = transform.position; // Đặt vị trí lần cuối bằng vị trí ban đầu
        StartCoroutine(Wander()); // Bắt đầu hành vi di chuyển
    }

    private void Update()
    {
        if (isMoving)
        {
            // Di chuyển Enemy tới vị trí mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Kiểm tra hướng di chuyển để lật mặt
            if (transform.position.x > lastPosition.x)
            {
                // Di chuyển sang phải -> Quay mặt sang phải
                transform.localScale = new Vector3(-3, 3, 3);
            }
            else if (transform.position.x < lastPosition.x)
            {
                // Di chuyển sang trái -> Quay mặt sang trái
                transform.localScale = new Vector3(3, 3, 3);
            }

            // Cập nhật vị trí lần cuối
            lastPosition = transform.position;

            // Dừng di chuyển nếu đã đến gần vị trí mục tiêu
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            if (!isMoving)
            {
                // Tạo một vị trí ngẫu nhiên trong phạm vi cho phép
                float randomX = Random.Range(-movementRange.x, movementRange.x);
                float randomY = Random.Range(-movementRange.y, movementRange.y);

                targetPosition = new Vector3(startPosition.x + randomX, startPosition.y + randomY, startPosition.z);

                // Bắt đầu di chuyển
                isMoving = true;

                // Đợi một thời gian trước khi tiếp tục di chuyển
                yield return new WaitForSeconds(waitTime);
            }

            yield return null;
        }
    }
}
