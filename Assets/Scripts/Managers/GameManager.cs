using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public Transform PlayerTransform;
    public PostProccesControl PostProccesControl;
    public AudioSource audioSource;

    CarControllerLudum player;

    [Header("Varriables")]
    public float gameOverLerpMulti = .5f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        SetReferances();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        GameOver();
    }

    void SetReferances()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PostProccesControl = GetComponent<PostProccesControl>();
        player = PlayerTransform.gameObject.GetComponent<CarControllerLudum>();
    }

    void GameOver()
    {
        if(!player.isAlive)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, 0, gameOverLerpMulti * Time.deltaTime);
        }
    }
}
