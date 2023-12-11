using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GetVelocity : MonoBehaviour
{

    public Rigidbody carRb;
    [SerializeField] VisualEffect visualEffect;
    public float VelocityDivadeX = 10;
    public float VelocityDivadeY = 10;
    public float VelocityDivadeZ = 10;
    private void Start()
    {

    }

    private void Update()
    {
        visualEffect.SetVector3("Velocity", -new Vector3(carRb.velocity.x / VelocityDivadeX, carRb.velocity.y / VelocityDivadeY, carRb.velocity.z / VelocityDivadeZ));
    }
}
