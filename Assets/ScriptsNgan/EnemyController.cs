using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f; // Máu của quái vật

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} nhận {damage} sát thương. Máu còn: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} đã bị tiêu diệt!");
        Destroy(gameObject);
    }
}
