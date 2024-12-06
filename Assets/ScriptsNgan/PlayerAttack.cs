using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f; // Phạm vi tấn công (chỉ kiếm)
    public float attackDamage = 20f; // Sát thương mỗi đòn đánh
    public float maxAttackDistance = 2f; // Tầm đánh tối đa (chỉ kiếm)
    public LayerMask enemyLayer; // Layer chứa quái vật

    public GameObject arrowPrefab; // Prefab mũi tên
    public Transform shootPoint;   // Vị trí bắn cung
    public float arrowSpeed = 10f; // Tốc độ mũi tên
    public float arrowDamage = 20f; // Sát thương cho mũi tên


    public Animator animator; // Tham chiếu Animator để chạy hoạt ảnh

    public bool isUsingBow = false; // Kiểm tra trạng thái vũ khí
    public Camera playerCamera; // Camera của người chơi

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0)) // Nhấn chuột trái để tấn công
        {
            if (isUsingBow)
            {
                ShootArrow(); // Tấn công bằng cung
            }
            else
            {
                SwordAttack(); // Tấn công bằng kiếm
            }
        }
    }

    private void SwordAttack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        // Kiểm tra nếu con trỏ chuột nằm trong tầm đánh tối đa
        float distanceToMouse = Vector3.Distance(transform.position, mousePosition);
        if (distanceToMouse <= maxAttackDistance)
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(mousePosition, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>()?.TakeDamage(attackDamage);
            }

            Debug.Log($"Tấn công bằng kiếm tại {mousePosition}. Số quái bị trúng: {hitEnemies.Length}");
        }
    }

    // Hàm bắn mũi tên
    public void ShootArrow()
    {
        if (arrowPrefab && shootPoint)
        {
            // Tạo mũi tên tại vị trí shootPoint
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);

            // Lấy Rigidbody2D của mũi tên để áp dụng lực
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Tính toán hướng từ shootPoint đến vị trí chuột
                Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Loại bỏ trục Z, vì chúng ta chỉ làm việc với 2D

                // Tính toán hướng bắn (vector hướng từ shootPoint đến chuột)
                Vector2 direction = (mousePosition - shootPoint.position).normalized;

                // Áp dụng tốc độ cho mũi tên theo hướng tính toán
                rb.velocity = direction * arrowSpeed;

                // Xoay mũi tên theo hướng bắn (theo vector vận tốc)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Tính toán góc theo radian
                arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Áp dụng góc xoay vào mũi tên
            }

        }
    }

    // Xử lý va chạm cho mũi tên
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Kiểm tra nếu mũi tên va vào đối tượng có layer "Enemy"
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // Kiểm tra layer "Enemy"
    //     {
    //         // Lấy EnemyController từ quái vật để gọi phương thức TakeDamage
    //         EnemyController enemy = other.GetComponent<EnemyController>();
    //         if (enemy != null)
    //         {
    //             // Gây sát thương cho enemy
    //             enemy.TakeDamage(attackDamage);
    //             Debug.Log("Mũi tên trúng quái vật!");
    //         }

    //         // Sau khi mũi tên va chạm với enemy, bạn có thể hủy mũi tên đi
    //         Destroy(gameObject); // Hủy mũi tên sau khi va chạm
    //     }
    // }
}
