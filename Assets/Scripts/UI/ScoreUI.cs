using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    GameManager gameManager;

    public TextMeshProUGUI scoreValue;

    public GameObject ComboTextObj;
    public TextMeshProUGUI comboValue;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshUI();
        ComboUI();
    }


    void RefreshUI()
    {
        scoreValue.text = gameManager.currentScore.ToString("F2");
    }
    int comboCounter;
    void ComboUI()
    {
        comboCounter = gameManager.player.GetComponentInChildren<CarScoreSystem>().comboCounter;
        Debug.Log("ComboCounterSCOREUI -Z"+comboCounter);
        if (comboCounter > 1)
        {
            ComboTextObj.SetActive(true);
            comboValue.text = "Combo X" + comboCounter;
        }
        else if(comboCounter < 1)
        {
            ComboTextObj.SetActive(false);
        }
    }
}
