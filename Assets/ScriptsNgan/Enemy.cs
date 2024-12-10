using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string questName = "Chiến đấu với quái vật"; // Tên nhiệm vụ liên quan đến quái vật này
    public int killValue = 1; // Số lượng tăng lên khi giết quái vật
    private QuestManager questManager;

    void Start()
    {
        // Tìm QuestManager trong scene
        questManager = FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager không được tìm thấy!");
        }
    }

    public void TakeDamage(int damage)
    {
        // Giảm máu quái vật, ví dụ nếu máu <= 0 thì chếts
        int health = 0; // (Thêm logic máu của bạn ở đây)
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
{
    if (questManager != null)
    {
        // Cập nhật nhiệm vụ "Chiến đấu với quái vật"
        questManager.UpdateQuestProgress("Chiến đấu với quái vật", 1);
        Debug.Log("Quái vật bị tiêu diệt, nhiệm vụ được cập nhật.");
    }

    Destroy(gameObject); // Hủy đối tượng quái vật
}





}
