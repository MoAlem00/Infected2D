using UnityEngine;
using UnityEngine.SceneManagement;

//this script handles game states and menus
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private GameObject gameStoryPanel;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private AssaultRifle gun;
    
    private bool instructionShowing = true;

    private void Start()
    {
        StartCoroutine(SoundsManager.Instance.PlayShuffleMusic());//shuffle music when game starts
        ShowGameStoryPanel();//show game story panel
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !instructionShowing)//if P or ESC pressed
        {
            PauseGame(); //show pause menu 
            if(settingsMenu.activeSelf) //if setting panel is active and player press P or ESC -> go back
                settingsMenu.SetActive(false);
        }
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void PauseGame()//show using P or ESC
    {
        Cursor.visible = true; 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gun.enabled = false; //to fix a bug: when i pause and press resume the player will shoot.
    }

    public void ShowLosePanel()//show when player dies
    {
        //it shows max wave reached and total enemies killed
        Cursor.visible = true;
        UIManager.Instance.ShowResultText(waveManager.waveRound,waveManager.totalEnemiesKilled);
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()//resume the game from pause menu
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gun.enabled = true; //to fix a bug: when i pause and press resume the player will shoot.
    }

    public void RestartGame()//restart the game
    {
        Cursor.visible = false;
        losePanel.SetActive(false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    
    public void OpenSettings()//opens settings menu
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackToMenu()//return from settings menu to pause menu
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void ShowGameStoryPanel()//show game story and controls
    {
        gun.enabled = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        gameStoryPanel.SetActive(true);
    }

    public void HideGameStoryPanel()//hide game story panel and go to the instructions
    {
        gameStoryPanel.SetActive(false);
        ShowInstructionPanel();
    }

    private void ShowInstructionPanel()//show instruction at the start of the game
    {
        Cursor.visible = true;
        instructionPanel.SetActive(true);
        Time.timeScale = 0;
        instructionShowing = true;
    }

    public void HideInstructionPanel()//hide the instructions
    {
        Cursor.visible = false;
        instructionPanel.SetActive(false);
        Time.timeScale = 1;
        instructionShowing = false;
        gun.enabled = true;
    }

}
