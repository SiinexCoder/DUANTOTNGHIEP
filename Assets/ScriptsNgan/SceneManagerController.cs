using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    void Start()
    {
        // Khi bắt đầu trò chơi, tải lại dữ liệu inventory
        InventoryManager.Instance.LoadInventory();
    }

    public void OnSceneChange(string sceneName)
    {
        // Lưu lại inventory trước khi chuyển scene
        InventoryManager.Instance.SaveInventory();

        // Chuyển scene
        SceneManager.LoadScene(sceneName);
    }

    public void StartBossFight()
    {
        // Chuyển đến vòng boss mà không reset inventory
        SceneManager.LoadScene("Boss");
    }
}
