using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    GameManager gameManager;
    CarControllerLudum player;
    public GameObject GameOverBackGround;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
        player = gameManager.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive)
        {
            ActivateGameOverMenu();
        }
    }

    void ActivateGameOverMenu()
    {
        GameOverBackGround.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TestMainMenu");
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
