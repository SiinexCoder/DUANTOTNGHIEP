using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_monter : MonoBehaviour
{
    public List<LootItem> lootItems;
    public float dropDistance = 1.0f;
    [System.Serializable]
    public class LootItem
    {
        public GameObject prefab; // prefab của món đồ
        [Range(0, 1)]
        public float dropChance; // tỉ lệ rơi đồ của món đồ
    }
    public int health = 10; // Số máu của quái
    public int damage = 1; // Số máu bị trừ mỗi lần va chạm với Player
    private bool canDamage = true; // Biến kiểm tra xem quái có thể gây sát thương hay không

    private Animator animator; // Tham chiếu tới Animator
    private SpriteRenderer spriteRenderer; // Tham chiếu tới SpriteRenderer
    private AudioSource audioSource; // Tham chiếu tới AudioSource
    public AudioClip damageSound; // Âm thanh phát khi nhận sát thương
    public AudioClip deathSound; // Âm thanh phát khi chết
    public Color damagedColor = Color.red; // Màu khi bị sát thương
    private Color originalColor; // Lưu màu ban đầu

    private QuestManager questManager; // Tham chiếu đến QuestManager

    public string questName = "Giết quái vật"; // Tên nhiệm vụ liên quan

    public int enemyValue = 1; // Số lượng quái vật đóng góp cho nhiệm vụ

    private void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator từ chính GameObject này
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer
        audioSource = GetComponent<AudioSource>(); // Lấy AudioSource

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Lưu màu ban đầu
        }

        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("Arrow") && canDamage)
        {
            TakeDamage(damage); // Gọi hàm giảm máu
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount; // Trừ máu

        // Đổi màu và phát âm thanh khi nhận sát thương
        if (spriteRenderer != null)
        {
            StartCoroutine(ChangeColorOnDamage());
        }
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound); // Phát âm thanh sát thương
        }

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

        // Phát âm thanh khi chết
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Gọi trigger "OnDied" trong Animator
        if (animator != null)
        {
            animator.SetTrigger("OnDied");
        }


        DropLoot();
        if (questManager != null)
        {
            questManager.UpdateQuestProgress(questName, enemyValue); // Cập nhật nhiệm vụ giết quái vật
        }
        // Phá hủy game object sau khi chạy animation (tùy ý)
        Destroy(gameObject, 0.1f); // Để 1 giây trước khi xóa quái (hoặc chỉnh sửa tùy ý)
    }

    private IEnumerator ResetDamageAbility()
    {
        canDamage = false; // Không thể gây sát thương
        yield return new WaitForSeconds(1f); // Chờ 1 giây
        canDamage = true; // Bật lại khả năng gây sát thương
    }

    private IEnumerator ChangeColorOnDamage()
    {
        spriteRenderer.color = damagedColor; // Đổi sang màu bị sát thương
        yield return new WaitForSeconds(0.2f); // Chờ 0.2 giây
        spriteRenderer.color = originalColor; // Đổi lại màu ban đầu
    }

    private void DropLoot()
    {
        // Tạo một số ngẫu nhiên từ 0 đến 1 cho từng item
        foreach (LootItem lootItem in lootItems)
        {
            if (Random.value < lootItem.dropChance)
            {
                // Nếu tỉ lệ rơi đồ đạt yêu cầu, tạo món đồ
                Vector3 randomPosition = transform.position + new Vector3(
                    Random.Range(-dropDistance, dropDistance),
                    Random.Range(-dropDistance, dropDistance),
                    0); // Không thay đổi trục Z

                Instantiate(lootItem.prefab, randomPosition, Quaternion.identity);
            }
        }
    }
}
