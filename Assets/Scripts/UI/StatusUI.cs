using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class StatusUI : MonoBehaviour
{
    GameManager gameManager;

    public TextMeshProUGUI speedValue;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        speedValue.text = gameManager.player.currentSpeed.ToString("F2");
    }
}
