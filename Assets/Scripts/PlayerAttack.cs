using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f; // Phạm vi tấn công
    public float attackDamage = 20f; // Sát thương mỗi đòn đánh
    public float maxAttackDistance = 2f; // Tầm đánh tối đa
    public LayerMask enemyLayer; // Layer chứa quái vật

    public Animator animator; // Tham chiếu Animator để chạy hoạt ảnh

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        // Kiểm tra nhấn chuột phải để tấn công
        if (Input.GetMouseButtonDown(0))
        {
            // Lấy vị trí của con trỏ chuột
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            // Kiểm tra nếu con trỏ chuột nằm trong tầm đánh tối đa
            float distanceToMouse = Vector3.Distance(transform.position, mousePosition);
            if (distanceToMouse <= maxAttackDistance)
            {
                // Kích hoạt hoạt ảnh tấn công
                animator.SetTrigger("Attack");

                Attack(mousePosition);
            }
            else
            {
                Debug.Log("Vị trí tấn công nằm ngoài tầm đánh!");
            }
        }
    }

    private void Attack(Vector3 attackPosition)
    {
        // Tìm tất cả quái vật trong phạm vi tấn công
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);

        // Gây sát thương cho từng quái
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>()?.TakeDamage(attackDamage);
        }

        Debug.Log($"Tấn công tại vị trí {attackPosition}. Số quái bị trúng: {hitEnemies.Length}");
    }

    // Hiển thị phạm vi tấn công và tầm đánh tối đa trong Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 mousePosition = Camera.main != null ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : transform.position;
        mousePosition.z = transform.position.z;

        // Vẽ phạm vi tấn công tại vị trí con trỏ chuột
        Gizmos.DrawWireSphere(mousePosition, attackRange);

        // Vẽ tầm đánh tối đa từ vị trí nhân vật
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxAttackDistance);
    }
}
