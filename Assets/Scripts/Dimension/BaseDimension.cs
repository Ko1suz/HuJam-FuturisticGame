using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseDimension : MonoBehaviour
{
    public string dimensonName;
    public static float current_Z_pos;
    public int partCount;
    public int part_X_Lenght;
    public int part_Y_Lenght;
    public int part_Z_Lenght;

    public float spawnOffset = 500;
    [Header("Refs")]
    public Transform player;

    [Header("Prefabs")]
    public GameObject partPrefab;
    public GameObject sunPrefab;


    [Header("CurrentValues")]
    public List<GameObject> currentParts;



    private void Awake()
    {
        
    }
    protected virtual void Start()
    {
        SpawnParts();
        Call_a_Part();
    }

    protected virtual void Update()
    {
        if (currentParts.Last().transform.position.z < player.position.z + spawnOffset) 
        {
            Call_a_Part();
        }
    }

  
    void SpawnParts()
    {
        for (int i = 0; i < partCount; i++)
        {
            GameObject clonePart = new GameObject();
            GameObject partCLone = Instantiate(partPrefab);
            clonePart.transform.parent = this.gameObject.transform;
            partCLone.transform.parent = clonePart.transform;
            partCLone.SetActive(true);
            clonePart.SetActive(false);
            currentParts.Add(clonePart);
        }
        GameObject sunPrefabClone =  Instantiate(sunPrefab);
        sunPrefabClone.transform.parent = this.gameObject.transform;
        sunPrefabClone.transform.localPosition = new Vector3(0,300,0);    
    }
    void Call_a_Part()
    {
        GameObject callingPart = currentParts[0];
        callingPart.SetActive(true);
        callingPart.transform.position = new Vector3(0,0, current_Z_pos);
        current_Z_pos += part_Z_Lenght;
        currentParts.Remove(callingPart);
        currentParts.Add(callingPart);
    }

    void CheckPlayerPositon()
    {

    }
}
