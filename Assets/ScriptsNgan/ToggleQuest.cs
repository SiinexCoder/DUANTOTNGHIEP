using UnityEngine;

public class ToggleQuest : MonoBehaviour
{
    public GameObject questPanel; 
    public GameObject questText;   
    private bool isQuestVisible = false; 

    // Hàm bật/tắt giao diện balo
    public void ToggleQuestUI()
    {
        isQuestVisible = !isQuestVisible; // Đảo ngược trạng thái
        questPanel.SetActive(isQuestVisible); // Hiển thị hoặc ẩn panel chứa item
        questText.SetActive(isQuestVisible);    // Có thể để ẩn nếu không muốn UI phụ xuất hiện
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleQuestUI();
        }
    }
    
}
