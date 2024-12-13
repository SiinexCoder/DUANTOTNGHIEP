using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;       // Tốc độ di chuyển của quái vật
    public float chaseRange = 10f; // Khoảng cách mà quái vật bắt đầu đuổi theo
    public float health = 100f;    // Máu của quái vật
    public GameObject[] lootItems; // Mảng các item mà quái vật có thể rơi ra
    public float dropChance = 0.5f; // Tỉ lệ rơi item (mặc định là 50%)

    private Color originalColor;   // Màu ban đầu của quái vật

    private Renderer rend;         // Biến lưu Renderer để thay đổi màu sắc
    private Transform player;      // Biến lưu tham chiếu tới nhân vật
    private Animator animator;     // Tham chiếu đến Animator để điều khiển animation
    private AudioSource audioSource; // AudioSource chứa âm thanh bị thương

    public AudioClip hurtSound;


    private void Start()
    {
        // Lấy Renderer và Animator của quái vật
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color; // Lưu màu ban đầu
        animator = GetComponent<Animator>(); // Lấy Animator của quái vật

        // Tìm nhân vật có Tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Kiểm tra nếu player đã được tìm thấy
        if (player == null) return;

        // Kiểm tra khoảng cách giữa quái vật và nhân vật
        float distance = Vector2.Distance(transform.position, player.position);

        // Nếu khoảng cách nhỏ hơn hoặc bằng khoảng cách đuổi theo, quái vật sẽ đuổi theo
        if (distance <= chaseRange)
        {
            // Tính toán hướng di chuyển
            Vector2 direction = (player.position - transform.position).normalized;

            // Di chuyển quái vật theo hướng đó
            transform.Translate(direction * speed * Time.deltaTime);

            // Xoay quái vật theo hướng di chuyển
            Flip(direction);

            // Kích hoạt animation di chuyển
            animator.SetBool("IsMoving", true); // Set parameter "IsMoving" trong Animator
        }
        else
        {
            // Nếu không còn đuổi theo player, quái vật dừng lại
            animator.SetBool("IsMoving", false); // Set parameter "IsMoving" về false
        }
    }

    // Hàm xoay quái vật theo hướng di chuyển
    private void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-6, 6, 6);  // Xoay sang phải
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(6, 6, 6); // Xoay sang trái
        }
    }

    // Hàm xử lý va chạm với các đối tượng có tag "Sword" hoặc "Arrow"
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra quái vật va chạm với vũ khí
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Arrow"))
        {
            TakeDamage(10f); // Gây sát thương cho quái vật
        }

        if (collision.gameObject.CompareTag("Arrow"))
        {
            // Thêm lực đẩy lên quái vật khi va chạm với mũi tên
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                float pushForce = 1f; // Đoạn lực đẩy cho quái vật
                rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
            }
        }

        // Kiểm tra quái vật va chạm với player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Gây sát thương cho player (10 sát thương)
            }
        }
    }

    // Hàm giảm máu quái vật và thay đổi màu khi bị thương
    public void TakeDamage(float damage)
    {
        health -= damage; // Giảm máu
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound); // Phát âm thanh sát thương
        }
        // Đổi màu quái vật thành màu đỏ khi bị thương
        rend.material.color = Color.red;

        if (health <= 0)
        {
            Die(); // Quái vật chết
        }
        else
        {
            // Quái vật sẽ trở về màu ban đầu sau một thời gian ngắn
            Invoke("ResetColor", 0.2f); // Sau 0.2 giây
        }
    }

    // Quay lại màu ban đầu sau khi bị thương
    private void ResetColor()
    {
        rend.material.color = originalColor; // Quay lại màu ban đầu
    }

    // Hàm quái vật chết
    private void Die()
    {
        // Kiểm tra tỉ lệ rơi item và quyết định rơi item
        DropLoot();

        // Hủy quái vật
        Destroy(gameObject);
    }

    // Hàm rơi item ngẫu nhiên dựa trên tỉ lệ
    private void DropLoot()
    {
        if (lootItems.Length > 0)
        {
            // Chọn ngẫu nhiên một item từ mảng lootItems
            if (Random.Range(0f, 1f) <= dropChance)
            {
                int randomIndex = Random.Range(0, lootItems.Length);
                GameObject loot = lootItems[randomIndex];

                // Tạo item ở vị trí của quái vật
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
    }
}
