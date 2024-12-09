using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    // Biến lưu máu hiện tại của đối tượng
    [SerializeField]
    private float _currentHealth;

    // Biến lưu lượng máu tối đa của đối tượng
    [SerializeField]
    private float _maximumHealth;

    // Thuộc tính trả về phần trăm máu còn lại
    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth; // Tính tỷ lệ máu còn lại
        }
    }

    // Biến kiểm tra đối tượng có ở trạng thái bất tử (miễn nhiễm sát thương) hay không
    public bool IsInvincible { get; set; }

    // Sự kiện Unity được gọi khi đối tượng chết
    public UnityEvent OnDied;

    // Sự kiện Unity được gọi khi đối tượng bị sát thương
    public UnityEvent OnDamaged;

     [SerializeField]
    private float damageAmount = 1f; // Số máu bị trừ khi va chạm

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Tìm thành phần HealthController trên đối tượng Player
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            if (healthController != null)
            {
                // Gọi hàm trừ máu trên Player
                healthController.TakeDamage(damageAmount);
            }
        }
    }

    // Hàm xử lý khi đối tượng nhận sát thương
    public void TakeDamage(float damageAmount)
    {
        // Nếu máu đã bằng 0, không làm gì thêm
        if (_currentHealth == 0)
        {
            return;
        }

        // Nếu đối tượng đang ở trạng thái bất tử, bỏ qua sát thương
        if (IsInvincible)
        {
            return;
        }

        // Giảm máu hiện tại dựa trên lượng sát thương nhận được
        _currentHealth -= damageAmount;

        // Đảm bảo máu không giảm xuống dưới 0
        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        // Nếu máu bằng 0, gọi sự kiện OnDied
        if (_currentHealth == 0)
        {
            OnDied.Invoke();
        }
        else
        {
            // Nếu máu chưa về 0, gọi sự kiện OnDamaged
            OnDamaged.Invoke();
        }
    }

    // Hàm thêm máu cho đối tượng
    public void AddHealth(float amountToAdd)
    {
        // Nếu máu đã đầy, không cần thêm nữa
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        // Tăng lượng máu hiện tại lên
        _currentHealth += amountToAdd;

        // Đảm bảo máu không vượt quá lượng máu tối đa
        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }
}
