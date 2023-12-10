using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPlayerFallower : MonoBehaviour
{
    public Transform Player;
    public float zOffset;
    public float rotSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager._instance.PlayerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        setPos();
    }

    float zRot = 0;
    void setPos()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y , Player.transform.position.z + zOffset);
        transform.rotation = Quaternion.Euler(0,0, zRot);
        zRot += Time.deltaTime * rotSpeed;
    }
}
