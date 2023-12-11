using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    
    public void Oyna()
    {
        SceneManager.LoadScene("MusicSyncScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
