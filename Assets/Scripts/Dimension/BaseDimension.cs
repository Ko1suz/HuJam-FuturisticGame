using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseDimension : MonoBehaviour
{
    protected GameManager gameManager = GameManager._instance;
    public string dimensonName;
    public static float current_Z_pos;
    public int partCount;
    public int part_X_Lenght;
    public int part_Y_Lenght;
    public int part_Z_Lenght;

    public float spawnOffset = 500;
    [Header("Refs")]
    public Transform player;
    [SerializeField] public Material skybox;
    public AudioClip[] dimensionMusics;

    [Header("Prefabs")]
    public GameObject partPrefab;
    public float sunYoffset = 300;
    public float sunXoffset = 0;
    public GameObject sunPrefab;
    //public GameObject reflectionProbePrefab;

    [Header("CurrentValues")]
    public List<GameObject> currentParts;
    protected GameObject currentSun;


    private void Awake()
    {
        
    }
    protected virtual void OnEnable()
    {
        RenderSettings.skybox = skybox;
        gameManager.audioSource.clip = ReturnDimensionSong();
        gameManager.audioSource.Play();
    }
    public AudioClip ReturnDimensionSong()
    {
        return dimensionMusics[Random.Range(0, dimensionMusics.Length)];
    }
    protected virtual void OnDisable()
    {
        for (int i = 0; i < currentParts.Count; i++)
        {
            currentParts[i].SetActive(false);
            currentParts[i].transform.position = Vector3.zero;
        }
    }
    protected virtual void Start()
    {
        player = GameManager._instance.PlayerTransform;
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
        if (sunPrefab != null)
        {
            GameObject sunPrefabClone = Instantiate(sunPrefab);
            sunPrefabClone.transform.parent = this.gameObject.transform;
            sunPrefabClone.transform.localPosition = new Vector3(sunXoffset, sunYoffset, 0);
            sunPrefabClone.SetActive(true);
            currentSun = sunPrefabClone;
        }
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
