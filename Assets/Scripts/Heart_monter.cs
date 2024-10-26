using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_monter : MonoBehaviour
{
     public int health = 10; // Số máu của quái
    public int damage = 1; // Số máu bị trừ mỗi lần va chạm với Player
    private bool canDamage = true; // Biến kiểm tra xem quái có thể gây sát thương hay không

    
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            TakeDamage(damage); // Gọi hàm giảm máu
        }
    }

    private void TakeDamage(int amount)
    {
        health -= amount; // Trừ máu

        // Kiểm tra xem quái có còn máu không
        if (health <= 0)
        {
            Die(); // Gọi hàm chết nếu máu bằng 0 hoặc nhỏ hơn
        }
        else
        {
            StartCoroutine(ResetDamageAbility()); // Bắt đầu coroutine để reset khả năng gây sát thương
        }
    }

    private void Die()
    {
        // Xử lý khi quái chết (có thể thêm hiệu ứng, âm thanh, v.v.)
        Debug.Log("Monster died!");
        Destroy(gameObject); // Xóa quái khỏi scene
    }

    private IEnumerator ResetDamageAbility()
    {
        canDamage = false; // Không thể gây sát thương
        yield return new WaitForSeconds(1f); // Chờ 1 giây
        canDamage = true; // Bật lại khả năng gây sát thương
    }
}
