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
    public GameObject victoryPanel;  // Panel Chúc mừng
    public TMPro.TMP_Text victoryText; // Text hiển thị thông báo và đếm ngược


    void Start()
    {
        // Thêm các nhiệm vụ cho mỗi scene
        questsByScene.Add("Backup Scene 1", new List<Quest>
        {
            new Quest { questName = "Thu thập kim cương xanh", description = "Thu thập 5 viên kim cương xanh.", requiredItemCount = 5, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Thu thập kim cương đỏ", description = "Thu thập 5 viên kim cương đỏ.", requiredItemCount = 5, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Thu thập lá thuốc", description = "Thu thập 1 lá thuốc.", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Tìm vật phẩm bí ẩn", description = "Tìm vật phẩm bí ẩn", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            
        });

        questsByScene.Add("Backup Scene 2", new List<Quest>
        {
            new Quest { questName = "Tìm kiếm vật phẩm bí ẩn", description = "Tìm kiếm vật phẩm bí ẩn", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Thu thập kim cương đỏ", description = "Thu thập 5 viên kim cương đỏ.", requiredItemCount = 5, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Giết quái vật", description = "Tiêu diệt 20 quái vật", requiredItemCount = 20, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Thu thập thuốc hồi phục", description = "Thu thập 10 lọ thuốc", requiredItemCount = 10, currentItemCount = 0, isCompleted = false }
        });

        questsByScene.Add("Backup Scene 3", new List<Quest>
        {
            new Quest { questName = "Tìm kiếm kho báu", description = "Tìm kiếm kho báu", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Đánh bại quái vật", description = "Giết 30 quái vật", requiredItemCount = 30, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Thu thập vũ khí", description = "Thu thập thanh kiếm đặc biệt", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Cứu dân làng", description = "Cứu 10 dân làng", requiredItemCount = 10, currentItemCount = 0, isCompleted = false }
        });

        questsByScene.Add("Backup Scene 4", new List<Quest>
        {
            new Quest { questName = "Giải cứu công chúa", description = "Giải cứu công chúa", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Đánh bại trùm", description = "Đánh bại trùm cuối", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Thu thập vũ khí mạnh", description = "Thu thập 1 thanh vũ khí mạnh", requiredItemCount = 1, currentItemCount = 0, isCompleted = false },
            new Quest { questName = "Chạy thoát", description = "Thoát khỏi hang động", requiredItemCount = 1, currentItemCount = 0, isCompleted = false }
        });

        victoryPanel.SetActive(false); // Ẩn panel chúc mừng khi bắt đầu

        LoadQuestsForCurrentScene();
    }

    void LoadQuestsForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Kiểm tra nếu scene có nhiệm vụ
        if (questsByScene.ContainsKey(currentScene))
        {
            currentSceneQuests = questsByScene[currentScene];
            UpdateQuestUI(); // Cập nhật UI với nhiệm vụ mới
        }
    }

    public void UpdateQuestProgress(string questName, int itemCount)
{
    Quest quest = currentSceneQuests.Find(q => q.questName == questName);
    if (quest != null)
    {
        quest.currentItemCount = itemCount;

        if (quest.currentItemCount >= quest.requiredItemCount)
        {
            quest.isCompleted = true;
            Debug.Log(quest.questName + " đã hoàn thành!\n");
        }

        // Kiểm tra nếu tất cả nhiệm vụ đã hoàn thành
        if (AllQuestsCompleted())
        {
            StartCoroutine(ShowCompletionNotification());
        }

        UpdateQuestUI();
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

    private IEnumerator ShowCompletionNotification()
{
    // Hiển thị panel chúc mừng và thông báo
    victoryPanel.SetActive(true);
    victoryText.text = "Nhiệm vụ hoàn thành! Chuyển cảnh sau 3 giây...";

    // Đếm ngược 3 giây
    int countdown = 3;
    while (countdown > 0)
    {
        victoryText.text = $"Nhiệm vụ hoàn thành! \n Chuyển cảnh sau {countdown} giây...";
        yield return new WaitForSeconds(1f);
        countdown--;
    }

    // Sau khi đếm ngược, chuyển cảnh
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
    else
    {
        Debug.Log("Chúc mừng bạn đã hoàn thành trò chơi!");
    }
}


    // Cập nhật UI với danh sách nhiệm vụ
    public void UpdateQuestUI()
    {
        questText.text = "Danh sách nhiệm vụ:\n";
        foreach (Quest quest in currentSceneQuests)
        {
            string status = quest.isCompleted ? " (Đã hoàn thành)" : $" ({quest.currentItemCount}/{quest.requiredItemCount})";
            questText.text += $"{quest.questName}: {quest.description}{status}\n";
        }
    }

    // Mở/đóng bảng nhiệm vụ khi nhấn Tab
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
    }
    
}
