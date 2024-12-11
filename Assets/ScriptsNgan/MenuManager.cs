using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    void Awake()
{
    // Đảm bảo rằng MenuManager không bị phá hủy khi chuyển cảnh
    DontDestroyOnLoad(gameObject);
}   
    public void StartGame()
    {
        // Chuyển đến scene trò chơi đầu tiên
        SceneManager.LoadScene("Backup Scene 1");
    }

    public void QuitGame()
    {
        // Thoát trò chơi
        Application.Quit();
    }

    public IEnumerator EndGameAndReturnToMenu()
    {
        // Sau khi hoàn thành trò chơi, chuyển về MainMenu
        yield return new WaitForSeconds(3f); // Đợi 3 giây
        SceneManager.LoadScene("MenuScene");
    }
}
