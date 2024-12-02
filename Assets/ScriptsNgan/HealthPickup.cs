using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20; // Số máu hồi được

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra xem có phải nhân vật không
        {
            PlayerHeath playerHealth = collision.GetComponent<PlayerHeath>(); // Lấy tham chiếu đến script PlayerHeath
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // Gọi hàm hồi máu
                Destroy(gameObject); // Xóa vật phẩm sau khi ăn
            }
        }
    }
}
