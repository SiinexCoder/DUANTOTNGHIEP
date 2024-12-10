using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    public AudioClip hurtSound; // Âm thanh bị thương
    private AudioSource audioSource; // Nguồn phát âm thanh

    private Animator animator; // Tham chiếu Animator

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(currentHealth, maxHealth);

        // Tham chiếu tới AudioSource
        audioSource = transform.Find("HurtAudio").GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource không được tìm thấy!");
        }

        // Tham chiếu Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator không được tìm thấy!");
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Tăng máu hiện tại
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Đảm bảo không vượt quá máu tối đa
        }
        healthBar.UpdateBar(currentHealth, maxHealth); // Cập nhật thanh máu
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage được gọi!");

        currentHealth -= damage;

        // Phát âm thanh bị thương
        if (hurtSound != null && audioSource != null)
        {
            Debug.Log("Phát âm thanh bị thương...");
            audioSource.PlayOneShot(hurtSound);
        }
        else
        {
            Debug.LogError("HurtSound hoặc AudioSource không hợp lệ!");
        }

        // Kích hoạt animation bị thương
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        // Kiểm tra nếu máu <= 0
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player đã chết!");
        Destroy(gameObject); // Hủy nhân vật
    }
}
