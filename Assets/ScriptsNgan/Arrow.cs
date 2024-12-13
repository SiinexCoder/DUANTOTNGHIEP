using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public int arrowDamage = 20; // Sát thương của mũi tên
    public float destroyAfterTime = 5f; // Thời gian mũi tên sẽ tự hủy sau khi bắn (5 giây)

    private void Start()
    {
        // Bắt đầu coroutine hủy mũi tên sau một khoảng thời gian
        StartCoroutine(DestroyArrowAfterTime(destroyAfterTime));
    }

    // Nếu sử dụng 2D, thay đổi hàm này thành OnCollisionEnter2D
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Lấy component Heart_monter để gọi hàm TakeDamage
            Heart_monter heart_Monter = collision.gameObject.GetComponent<Heart_monter>();
            if (heart_Monter != null)
            {
                heart_Monter.TakeDamage(arrowDamage); // Gọi hàm TakeDamage() của quái vật
            }
            // Hủy mũi tên sau khi va chạm
            Destroy(gameObject); // Hủy mũi tên
        }
        else
        {
            // Hủy mũi tên nếu va chạm với các đối tượng không phải quái vật
            Destroy(gameObject);
        }
    }

    // Coroutine hủy mũi tên sau thời gian quy định
    private IEnumerator DestroyArrowAfterTime(float time)
    {
        yield return new WaitForSeconds(time);  // Đợi 5 giây (hoặc thời gian bạn quy định)
        Destroy(gameObject);  // Hủy mũi tên nếu không va chạm với bất kỳ đối tượng nào
    }
}
