using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class BaseNpc : MonoBehaviour
{
    GameManager gameManager = GameManager._instance;
    public float x_road_value = 10; //-20 -10 10 20
    public bool zPositveDirection = true;
    public float speed;

    Rigidbody rb;
    private void Awake()
    {
        rb= GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        DistanceController();
    }

    void Move()
    {
        rb.velocity = Vector3.forward * speed;
    }

    float distanceTimer = 0;
    void DistanceController()
    {
        distanceTimer += Time.deltaTime;
        if (distanceTimer > 1)
        {
            if (transform.position.z + 10 < gameManager.PlayerTransform.position.z)
            {
                this.gameObject.SetActive(false);
            }
            distanceTimer = 0; 
        }
    }
}
