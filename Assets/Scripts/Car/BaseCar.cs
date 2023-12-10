using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCar : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    private Rigidbody rb;

    public WheelCollider FL, FR;
    public WheelCollider RL, RR;

    public Transform FL_Transform, FR_Transform;
    public Transform RL_Transform, RR_Transform;

    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public float donmeDuzeltmeHizi = 5f; // Dönme tuþunu býraktýktan sonraki düzeltme hýzý
    public float maksimumDondurmaAci = 45f; // Maksimum dönme açýsý

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Acclerate();
        UpdateWheelPoses();
    }

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal -> "+ m_horizontalInput);
        m_verticalInput = Input.GetAxis("Vertical");
    }

    void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;

        // Maksimum dönme açýsýný kontrol // 0 - 20  ve 360 - 340
        if ((transform.rotation.eulerAngles.y >= 0 && transform.rotation.eulerAngles.y <= maksimumDondurmaAci) 
            || (transform.rotation.eulerAngles.y >= 360 - maksimumDondurmaAci && transform.rotation.eulerAngles.y <= 360))
        {
            m_steeringAngle = maxSteerAngle * m_horizontalInput;
        }
        else
        {
            m_steeringAngle = 0;
        }
        FL.steerAngle = m_steeringAngle;
        FR.steerAngle = m_steeringAngle;
    }

    void Acclerate()
    {
        FL.motorTorque = m_verticalInput * motorForce;
        FR.motorTorque = m_verticalInput * motorForce;
    }

    void UpdateWheelPoses()
    {
        UpdateWheelPose(FL, FL_Transform);
        UpdateWheelPose(FR, FR_Transform);
        UpdateWheelPose(RL, RL_Transform);
        UpdateWheelPose(RR, RR_Transform);
    }

    void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    void Update()
    {
        // Dönme tuþunu býraktýðýnda arabanýn düzlemine geri dönme
        if (m_horizontalInput == 0f)
        {
            float currentRotation = transform.localEulerAngles.y;

            // Dönmeye hangi yönde devam etmesi gerektiðini belirle
            float targetRotation = (currentRotation > 180f) ? 360f : 0f;

            float duzeltmeMiktari = Mathf.Lerp(currentRotation, targetRotation, donmeDuzeltmeHizi * Time.deltaTime);
            rb.angularVelocity = Vector3.zero; // Hýzlanmayý sýfýrla
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, duzeltmeMiktari, transform.localEulerAngles.z);
        }
    }
}
