using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public float arrowDamage = 20f; // Sát thương của mũi tên
    public float destroyAfterTime = 5f; // Thời gian mũi tên sẽ tự hủy sau khi bắn (5 giây)

    private void Start()
    {
        // Bắt đầu coroutine hủy mũi tên sau một khoảng thời gian
        StartCoroutine(DestroyArrowAfterTime(destroyAfterTime));
    }

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

    // Coroutine hủy mũi tên sau thời gian quy định
    private IEnumerator DestroyArrowAfterTime(float time)
    {
        yield return new WaitForSeconds(time);  // Đợi 5 giây (hoặc thời gian bạn quy định)
        Destroy(gameObject);  // Hủy mũi tên
    }
}
