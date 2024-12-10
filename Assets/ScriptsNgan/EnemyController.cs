using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f; // Sức khỏe của quái vật

    // Phương thức để nhận sát thương
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Quái vật bị tấn công! Máu còn lại: " + health);

        if (health <= 0)
        {
            Die(); // Quái vật chết khi máu còn 0
        }
    }
    

    // Phương thức chết của quái vật
    void Die()
    {
        // Thực hiện hành động khi quái vật chết
        Debug.Log("Quái vật chết!");
        Destroy(gameObject); // Hủy quái vật
    }
}

