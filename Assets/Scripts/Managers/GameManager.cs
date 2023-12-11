using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public Transform PlayerTransform;
    public PostProccesControl PostProccesControl;
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

    void SetReferances()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PostProccesControl = GetComponent<PostProccesControl>();
    }
}
