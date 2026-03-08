using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public GameObject pauseMenu;
    public GameObject losePanel;
    private WeaponShoot gun;
    private UIManager uiManager;
    private WaveManager waveManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        uiManager = GameObject.Find("HUD").GetComponent<UIManager>();
        gun = GameObject.Find("Gun").GetComponent<WeaponShoot>();
        StartCoroutine(SoundsManager.Instance.PlayShuffleMusic());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        Cursor.visible = true; 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gun.enabled = false; //to fix a bug: when i pause and press resume the player will shoot.
    }

    public void ShowLosePanel()
    {
        Cursor.visible = true;
        uiManager.ShowResultText(waveManager.maxWaveReached,waveManager.totalEnemiesKilled);
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gun.enabled = true; //to fix a bug: when i pause and press resume the player will shoot.
    }

    public void RestartGame()
    {
        Cursor.visible = false;
        losePanel.SetActive(false);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
