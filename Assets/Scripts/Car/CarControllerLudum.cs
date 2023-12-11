using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CarControllerLudum : MonoBehaviour
{
    GameManager gameManager = GameManager._instance;
    public Rigidbody rb;
    public bool isAlive = true;
    [Header("Speed")]
    public float currentSpeed;
    public float defaultSpeed = 45f;
    public float thurstSpeed = 200;
    public float BoostSpeed = 500;
    public float thurstMaxVelocity = 100;
    public float boostMaxVelocity = 200;
    //Speed

    [Header("Rotate")]
    public float turnAngle = 60f;
    public float rotateAngle = 45f;
    public float turnSpeed = 5f;
    public float maxRotateAngle = 25f;
    public float minRotateAngle = -25f;

    public float maxY = 1;
    public float minY = -4f;

    public float max_X = 30;
    public float min_X = -30;


    public float pitchPower, rollPower, yawPower, enginePower;

    private float activeRoll, activePitch, activeYaw;


    public bool invertControls = true;
    public KeyCode nitroKey = KeyCode.Space;
    public bool canUseNitro;

    bool m_DetectDown;
    RaycastHit m_DownCheck;
    public float groundCheckLength = 1f;
    public LayerMask groundLayer;

    private void OnEnable()
    {
        isAlive = true;
    }
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        isAlive = true;
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            Turn();
            Thrust();
        }
        else
        {
            rb.useGravity = true;
            minY = -2;
        }
        LimitPosition();

        m_DetectDown = Physics.Raycast(transform.position, -transform.up, out m_DownCheck, groundCheckLength, groundLayer);
        // Debug.Log(Time.timeSinceLevelLoad);
    }
    void Turn()
    {
        float yaw = turnAngle * Input.GetAxis("Horizontal");
        float roll = rotateAngle * Input.GetAxis("Horizontal");
        if (m_DetectDown)
        {
            Quaternion targetRot = Quaternion.Euler(0, yaw, roll);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion targetRot = Quaternion.Euler(0, yaw, roll);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
        }



        if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            // Input yok ise arabayı orjinal rotasyonuna sokuyor // Ayrıca çarpınca arabanın sapıtmasını engelliyor
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetAxis("Horizontal")>0)
        {
            rb.AddForce(transform.right * currentSpeed/2);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(-transform.right * currentSpeed/2);
        }
    }

    //void Turn()
    //{
    //    float yaw = turnAngle * Input.GetAxis("Horizontal");
    //    float pitch = turnAngle * Input.GetAxis("Vertical");
    //    float roll = rotateAngle * Input.GetAxis("Horizontal");
    //    if (invertControls)
    //    {
    //        pitch = pitch * -1;
    //    }
    //    else
    //    {
    //        pitch = pitch * 1;
    //    }

    //    if (m_DetectDown)
    //    {
    //        float v;
    //        if (pitch<0)
    //        {
    //            v = pitch;
    //        }
    //        else
    //        {
    //            v = 0;
    //        }
    //        Quaternion targetRot = Quaternion.Euler(v, yaw, roll);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        Quaternion targetRot = Quaternion.Euler(pitch, yaw, roll);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    //    }



    //    if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
    //    {
    //        // Input yok ise arabayı orjinal rotasyonuna sokuyor // Ayrıca çarpınca arabanın sapıtmasını engelliyor
    //        rb.angularVelocity = Vector3.zero;
    //    }
    //}

    void Thrust()
    {
        currentSpeed = rb.velocity.magnitude;
        if (Input.GetKey(KeyCode.W))
        {
            if (currentSpeed < thurstMaxVelocity)
            {
                rb.AddForce(transform.forward * thurstSpeed);
            }
        }
        else
        {
            if (currentSpeed < thurstMaxVelocity / 2)
            {
                rb.AddForce(transform.forward * defaultSpeed);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentSpeed < boostMaxVelocity)
            {
                rb.AddForce(transform.forward * BoostSpeed);
            }
        }
       
       // rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * (speed + (Time.timeSinceLevelLoad * 5)) * Time.deltaTime, .1f);
    }

    void LimitPosition()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min_X, max_X), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isAlive = false;
        }
    }
}
