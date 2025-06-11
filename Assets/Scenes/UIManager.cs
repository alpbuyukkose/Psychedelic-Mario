using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("1-1");
    }
    public void OpenSettings()
    {
    SceneManager.LoadScene("Settings",LoadSceneMode.Single);
    }
    public void ReturnToMainMenu()
{
    SceneManager.LoadScene("MainMenu");
}   
    public void QuitGame()
    {
    Application.Quit();
    Debug.Log("Quit Game");
    }
}
