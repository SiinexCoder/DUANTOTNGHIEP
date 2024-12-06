using UnityEngine;
using UnityEngine.Events;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] int maxHeath;
    int currentHeath;

    public HeathBar heathBar;

    public AudioClip hurtSound; // Âm thanh bị thương
    private AudioSource audioSource; // Nguồn phát âm thanh

    private void Start()
{
    currentHeath = maxHeath;
    heathBar.UpdateBar(currentHeath, maxHeath);

    // Tham chiếu tới AudioSource riêng
    audioSource = transform.Find("HurtAudio").GetComponent<AudioSource>();
    if (audioSource == null)
    {
        Debug.LogError("AudioSource không được tìm thấy!");
    }
}


    public void Heal(int healAmount)
    {
        currentHeath += healAmount; // Tăng máu hiện tại
        if (currentHeath > maxHeath)
        {
            currentHeath = maxHeath; // Đảm bảo không vượt quá sức khỏe tối đa
        }
        heathBar.UpdateBar(currentHeath, maxHeath); // Cập nhật thanh máu
    }

    public void TakeDam(int damage)
{
    Debug.Log("TakeDam được gọi!");

    currentHeath -= damage;

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

    if (currentHeath <= 0)
    {
        currentHeath = 0;
        Debug.Log("Player đã chết!");
    }
    heathBar.UpdateBar(currentHeath, maxHeath);
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Kiểm tra phím SPACE để thử nghiệm
        {
            TakeDam(20);
        }
    }
}
