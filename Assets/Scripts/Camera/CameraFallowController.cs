using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallowController : MonoBehaviour
{
    GameManager gameManager;
    Transform player;
    public Transform objectToFallow;
    public Vector3 offset;
    public Vector3 lookOffset;
    public float fallowSpeed = 10;
    public float lookSpeed = 10;


    private void Start()
    {
        gameManager = GameManager._instance;
        player = gameManager.PlayerTransform;
        objectToFallow = player;
    }
    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }


    public void LookAtTarget()
    {
        Vector3 _lookRotation = objectToFallow.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookRotation + lookOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
    }

    public void MoveToTarget()
    {
        Vector3 _targetPos = objectToFallow.position +
                             objectToFallow.forward * offset.z +
                             objectToFallow.right * offset.x +
                             objectToFallow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos , fallowSpeed * Time.deltaTime);
    }
}
