using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
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
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene("MenuScene");
    }
}
