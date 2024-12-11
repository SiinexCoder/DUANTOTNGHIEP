using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {   
        public string questName;
        public string description;
        public bool isCompleted;
        public int requiredItemCount;
        public int currentItemCount;
    }

    public Dictionary<string, List<Quest>> questsByScene = new Dictionary<string, List<Quest>>(); // Lưu trữ nhiệm vụ theo từng scene
    public List<Quest> currentSceneQuests = new List<Quest>(); // Nhiệm vụ của scene hiện tại

    public GameObject questPanel;
    public TMPro.TMP_Text questText;
    private bool isPanelActive = false;

    public GameObject EndGamePanel;

    public TMPro.TMP_Text EndGameText;

    public GameObject victoryPanel;  // Panel Chúc mừng
    public TMPro.TMP_Text victoryText; // Text hiển thị thông báo và đếm ngược

    void Start()
{
    // Thêm các nhiệm vụ cho từng scene
    questsByScene.Add("Backup Scene 1", new List<Quest>
    {
        new Quest { questName = "Nhặt kim cương xanh", description = "Thu thập 5 kim cương xanh.", requiredItemCount = 5, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Nhặt kim cương đỏ", description = "Thu thập 5 kim cương đỏ.", requiredItemCount = 5, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Nhặt lá thuốc", description = "Thu thập 1 lá thuốc.", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Nhặt bình thuốc", description = "Thu thập 3 bình thuốc.", requiredItemCount = 3, currentItemCount = 0, isCompleted = false }
    });

    questsByScene.Add("Backup Scene 2", new List<Quest>
    {
        new Quest { questName = "Nhặt vật phẩm bí ẩn", description = "Thu thập vật phẩm bí ẩn", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Nhặt kim cương đỏ", description = "Thu thập 5 viên đá đỏ.", requiredItemCount = 5, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Nhặt lá thuốc", description = "Thu thập 1 lá thuốc", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Nhặt thuốc hồi phục", description = "Thu thập 5 thuốc hồi phục", requiredItemCount = 5, currentItemCount = 0, isCompleted = false }
    });

    questsByScene.Add("Backup Scene 3", new List<Quest>
    {
        new Quest { questName = "Tìm kiếm kho báu", description = "Tìm kiếm kho báu", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Giết quái vật", description = "Giết 30 quái vật", requiredItemCount = 30, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Thu thập vũ khí", description = "Thu thập thanh kiếm đặc biệt", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
        new Quest { questName = "Cứu dân làng", description = "Cứu 10 dân làng", requiredItemCount = 10, currentItemCount = 0, isCompleted = false }
    });

    questsByScene.Add("Backup Scene 4", new List<Quest>
    {
        new Quest { questName = "Nhặt kim cương xanh", description = "Thu thập 5 kim cương xanh.", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
    });
     questsByScene.Add("Boss", new List<Quest>
    {
        new Quest { questName = "Nhặt thuốc giải", description = "Thu thập thuốc giải độc.", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
    });

    victoryPanel.SetActive(false); // Ẩn panel chúc mừng khi bắt đầu

    LoadQuestsForCurrentScene(); // Gọi hàm cập nhật nhiệm vụ khi vào scene
}


   void LoadQuestsForCurrentScene()
{
    string currentScene = SceneManager.GetActiveScene().name; // Lấy tên của scene hiện tại

    if (currentSceneQuests == null || currentSceneQuests.Count == 0)
    {
        if (questsByScene.ContainsKey(currentScene)) // Kiểm tra xem scene có nhiệm vụ hay không
        {
            currentSceneQuests = questsByScene[currentScene]; // Nạp nhiệm vụ từ scene
            Debug.Log("Danh sách nhiệm vụ đã được tải thành công.");
        }
        else
        {
            Debug.Log("Không có nhiệm vụ nào cho scene này.");
        }
    }
    UpdateQuestUI(); // Cập nhật UI nhiệm vụ
}


    public void UpdateQuestProgress(string questName, int addedCount)
{
    Quest quest = currentSceneQuests.Find(q => q.questName == questName);
    if (quest != null)
    {
        quest.currentItemCount += addedCount;

        if (quest.currentItemCount >= quest.requiredItemCount)
        {
            quest.currentItemCount = quest.requiredItemCount;
            quest.isCompleted = true;
        }

        UpdateQuestUI();
    }
    else
    {
        Debug.LogWarning($"Không tìm thấy nhiệm vụ: {questName}");
    }
}


    private bool AllQuestsCompleted()
    {
        foreach (Quest quest in currentSceneQuests)
        {
            if (!quest.isCompleted)
                return false;
        }
        return true;
    }

    public IEnumerator ShowCompletionNotification()
    {   
        victoryPanel.SetActive(true);
        victoryText.text = "Nhiệm vụ hoàn thành! Chuyển cảnh sau 3 giây...";

        int countdown = 3;
        while (countdown > 0)
        {
            victoryText.text = $"Nhiệm vụ hoàn thành! \n Chuyển cảnh sau {countdown} giây...";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Backup Scene 1")
        {
         
            SceneManager.LoadScene("Backup Scene 2");
        }
        else if (currentScene == "Backup Scene 2")
        {
            SceneManager.LoadScene("Backup Scene 3");
        }
        else if (currentScene == "Backup Scene 3")
        {
            SceneManager.LoadScene("Backup Scene 4");
        }
        else if (currentScene == "Backup Scene 4")
        {
         
            SceneManager.LoadScene("Boss");
        }
        else
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = "Nhiệm vụ hoàn thành!";
            Debug.Log("Chúc mừng bạn đã hoàn thành trò chơi!");
        }
    }

    public void UpdateQuestUI()
{
    questText.text = "Danh sách nhiệm vụ:\n<br>";
    foreach (Quest quest in currentSceneQuests)
    {
        string status = quest.isCompleted ? " (Đã hoàn thành)" : $" ({quest.currentItemCount}/{quest.requiredItemCount})";
        // Sử dụng <br> để tạo dòng mới giữa các nhiệm vụ
        questText.text += $"{quest.questName}: {quest.description}{status}<br><br>";
    }
}



    public void ToggleQuestPanel()
    {
        isPanelActive = !isPanelActive;
        questPanel.SetActive(isPanelActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleQuestPanel();
        }

        if (AllQuestsCompleted())
        {
            StartCoroutine(ShowCompletionNotification());
        }
    }
}
