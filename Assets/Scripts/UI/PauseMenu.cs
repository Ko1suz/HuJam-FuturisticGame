using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;
    CarControllerLudum player;
    bool gameStop = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
        player = gameManager.player;
        FixTimeAndControls(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && player.isAlive)
        {
            if (!gameStop)
            {
                BrakeTimeAndControls();
                pauseMenu.SetActive(true);
            }
            else
            {
                FixTimeAndControls();
                pauseMenu.SetActive(false);
            }
        }
    }

    public void OyunuDevamEttir()
    {
        FixTimeAndControls();
        pauseMenu.SetActive(false); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TestMainMenu");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    void BrakeTimeAndControls()
    {
        gameStop = true;
        Time.timeScale = 0f; // 1f
        gameManager.audioSource.pitch = 0;
    }

    void FixTimeAndControls()
    {
        gameStop = false;
        Time.timeScale = 1f; // 1f 
        gameManager.audioSource.pitch = 1;
    }
}
