using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDimension : MonoBehaviour
{
    public static float current_Z_pos;
    public int partCount;
    public int part_X_Lenght;
    public int part_Y_Lenght;
    public int part_Z_Lenght;
    [Header("Refs")]
    Transform player;

    [Header("Prefabs")]
    public GameObject partPrefab; 


    [Header("CurrentValues")]
    public List<GameObject> parts;





    void SpawnParts()
    {
        for (int i = 0; i < partCount; i++)
        {
            GameObject clonePart = new GameObject();
            clonePart.SetActive(false);
            parts.Add(clonePart);
        }
    }
    void CallPart()
    {

    }

    void CheckPlayerPositon()
    {

    }
}
