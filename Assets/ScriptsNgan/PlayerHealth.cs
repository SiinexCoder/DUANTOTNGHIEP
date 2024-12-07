using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    public AudioClip hurtSound; // Âm thanh bị thương
    private AudioSource audioSource; // Nguồn phát âm thanh

    private void Start()
{
    currentHealth = maxHealth;
    healthBar.UpdateBar(currentHealth, maxHealth);

    // Tham chiếu tới AudioSource riêng
    audioSource = transform.Find("HurtAudio").GetComponent<AudioSource>();
    if (audioSource == null)
    {
        Debug.LogError("AudioSource không được tìm thấy!");
    }
}


    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Tăng máu hiện tại
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Đảm bảo không vượt quá sức khỏe tối đa
        }
        healthBar.UpdateBar(currentHealth, maxHealth); // Cập nhật thanh máu
    }

    public void TakeDam(int damage)
{
    Debug.Log("TakeDam được gọi!");

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

    if (currentHealth <= 0)
    {
        currentHealth = 0;
        Destroy(gameObject); // Hủy nhân vật
        Debug.Log("Player đã chết!");
    }
    healthBar.UpdateBar(currentHealth, maxHealth);
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Kiểm tra phím SPACE để thử nghiệm
        {
            TakeDam(20);
        }
    }
}
