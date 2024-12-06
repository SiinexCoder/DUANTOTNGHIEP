using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowDamage = 20f; // Sát thương của mũi tên

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        // Gây sát thương cho quái vật
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(arrowDamage);
        }

        // Hủy mũi tên sau khi va chạm
        Destroy(gameObject); // Hủy mũi tên, không hủy nhân vật!
    }
}

}
