using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScoreSystem : MonoBehaviour
{
    GameManager gameManager;
    CarControllerLudum player;
    public int comboCounter = 0;
    public float comboTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
        player = gameManager.player;
    }


    private void Update()
    {
        ComboTimer();
        SpeedScore();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            comboCounter++;
            comboTimer = 0;
            Debug.LogError("ComboSayýsý ->" + comboCounter);
        }
    }

    float comboTimer = 0;
    void ComboTimer()
    {
        if (comboCounter>0)
        {
            if (comboTimer > comboTime)
            {
                gameManager.currentScore += comboCounter *100;
                comboCounter = 0;
            }
            else
            {
                comboTimer += Time.deltaTime;
            }
        }
        else
        {
            comboTimer = 0;
        }
    }

    void SpeedScore()
    {
        if (player.isAlive)
        {
            if (player.currentSpeed > 40 && player.currentSpeed < 70)
            {
                gameManager.currentScore += Time.deltaTime * 1;
            }
            else if (player.currentSpeed > 70)
            {
                gameManager.currentScore += Time.deltaTime * 3;
            }
        }
    }
}
