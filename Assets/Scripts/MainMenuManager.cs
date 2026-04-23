using UnityEngine;
using UnityEngine.SceneManagement;


//script for the main menu scene that handles main menu actions
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SoundsManager.Instance.PlayMenuMusic();//play menu music
    }
    
    
    public void StartGame()//starts the game
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    
}
