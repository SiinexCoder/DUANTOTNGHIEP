using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Player player; // Tham chiếu đến đối tượng Player
    public int minDamage = 10; // Giá trị sát thương tối thiểu
    public int maxDamage = 10; // Giá trị sát thương tối đa
    private int currentHealth; // Sức khỏe hiện tại của quái vật
    public int maxHealth = 100; // Sức khỏe tối đa của quái vật

    public float moveSpeed = 2f; // Tốc độ di chuyển của quái vật
    public float detectionRange = 3f; // Khoảng cách quái vật có thể phát hiện nhân vật
    public float attackRange = 1.5f; // Khoảng cách tấn công
    public float attackCooldown = 1f; // Thời gian giữa các lần tấn công

    public Animator animator; // Tham chiếu đến Animator để điều khiển animation

    private Vector2 randomTargetPosition; // Vị trí mục tiêu ngẫu nhiên mà quái vật sẽ di chuyển đến
    public float wanderRadius = 3f; // Bán kính mà quái vật có thể di chuyển ngẫu nhiên
    private float wanderTimer; // Thời gian đếm ngược cho việc di chuyển ngẫu nhiên
    private float attackTimer; // Thời gian còn lại trước khi quái vật có thể tấn công tiếp

    public float knockbackForce = 1f; // Lực đẩy quái vật khi bị chém

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo sức khỏe hiện tại bằng sức khỏe tối đa
        wanderTimer = Random.Range(2f, 5f); // Khởi tạo thời gian ngẫu nhiên cho việc di chuyển ngẫu nhiên
        SetRandomTargetPosition(); // Thiết lập vị trí mục tiêu ngẫu nhiên đầu tiên
        attackTimer = 0; // Khởi tạo timer tấn công
    }

    private void Update()
    {
        player = FindObjectOfType<Player>(); // Tìm kiếm đối tượng Player trong scene

        if (player != null) // Kiểm tra xem Player có tồn tại không
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position); // Tính khoảng cách từ quái vật đến Player

            if (distanceToPlayer <= detectionRange) // Nếu Player ở trong khoảng cách phát hiện
            {
                MoveTowardsPlayer(distanceToPlayer); // Di chuyển về phía Player

                if (distanceToPlayer <= attackRange && attackTimer <= 0) // Nếu Player ở trong khoảng cách tấn công và chưa tấn công lại
                {
                    DamagePlayer(); // Tấn công Player
                }
            }
            else
            {
                MoveToRandomPosition(); // Nếu Player ra khỏi khoảng phát hiện, di chuyển đến vị trí ngẫu nhiên
            }
        }
        else
        {
            MoveToRandomPosition(); // Nếu không có Player, di chuyển đến vị trí ngẫu nhiên
        }

        // Cập nhật timer tấn công
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime; // Giảm thời gian còn lại cho timer tấn công
        }
    }

    private void MoveTowardsPlayer(float distanceToPlayer)
    {
        Vector2 direction = (player.transform.position - transform.position).normalized; // Tính hướng di chuyển về phía Player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime); // Di chuyển quái vật về phía Player

        // Đảo ngược hướng quái vật dựa trên vị trí của Player
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-5, 5, 5); // Quái vật quay về phía bên trái
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(5, 5, 5); // Quái vật quay về phía bên phải
        }
    }

    private void MoveToRandomPosition()
    {
        wanderTimer -= Time.deltaTime; // Giảm thời gian đếm ngược cho việc di chuyển ngẫu nhiên
        if (wanderTimer <= 0) // Nếu timer đã hết
        {
            SetRandomTargetPosition(); // Thiết lập vị trí mục tiêu ngẫu nhiên mới
            wanderTimer = Random.Range(2f, 5f); // Khởi tạo lại thời gian cho lần di chuyển tiếp theo
        }

        transform.position = Vector2.MoveTowards(transform.position, randomTargetPosition, moveSpeed * Time.deltaTime); // Di chuyển quái vật đến vị trí mục tiêu ngẫu nhiên
    }

    private void SetRandomTargetPosition()
    {
        randomTargetPosition = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius; // Thiết lập vị trí mục tiêu ngẫu nhiên trong bán kính nhất định
    }

    void DamagePlayer()
    {
        if (player != null) // Kiểm tra xem Player có tồn tại không
        {
            int damage = UnityEngine.Random.Range(minDamage, maxDamage); // Tính sát thương ngẫu nhiên
            player.TakeDamage(damage); // Gọi phương thức TakeDamage của Player
            animator.SetTrigger("Attack"); // Kích hoạt animation tấn công
            attackTimer = attackCooldown; // Reset timer tấn công
        }
    }

    public void TakeDamageEnemy(int damage, Vector2 hitDirection)
    {
        currentHealth -= damage; // Giảm sức khỏe hiện tại của quái vật

        animator.SetTrigger("Hurt"); // Kích hoạt animation bị thương

        // Tính toán vị trí văng ra
        Vector2 knockback = hitDirection.normalized * knockbackForce; // Tính toán lực đẩy dựa trên hướng và lực
        transform.position += (Vector3)knockback; // Đẩy quái vật ra theo hướng bị đánh

        if (currentHealth <= 0) // Nếu sức khỏe hiện tại <= 0
        {
            Die(); // Gọi phương thức Die
        }
    }

    void Die()
    {
        Debug.Log("Enemy Dies"); // Ghi log khi quái vật chết
        
        animator.SetBool("IsDead", true); // Kích hoạt animation chết
        this.enabled = false; // Vô hiệu hóa script quái vật
        GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa collider của quái vật
        Destroy(gameObject, 1f); // Huỷ đối tượng quái vật sau 1 giây
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) // Nếu quái vật va chạm với đối tượng có tag "Player"
        {
            player = collision.GetComponent<Player>(); // Lấy tham chiếu đến Player
            // Không cần tấn công tại đây, chỉ phát hiện người chơi
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) // Nếu quái vật ra khỏi va chạm với đối tượng có tag "Player"
        {
            player = null; // Đặt lại tham chiếu đến Player
            // Không cần hủy việc tấn công tại đây
        }
    }
}
