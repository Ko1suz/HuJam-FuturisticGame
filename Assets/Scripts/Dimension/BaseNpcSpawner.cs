using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseNpcSpawner : MonoBehaviour
{
    GameManager gameManager = GameManager._instance;
    public int maxActiveNpc;
    public bool twoWayRoad = false;

    public float spawnZ_Ofsset = 400;
    public float spawnRate = 1f;
    public GameObject[] npcPrefabs;


    public List<GameObject> currentNpcs;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        for (int i = 0; i < currentNpcs.Count; i++)
        {
            currentNpcs[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateNpcs();

    }

    private void FixedUpdate()
    {
        CallNpc();
    }
    void CreateNpcs()
    {
        for (int i = 0; i < npcPrefabs.Length; i++)
        {
            for (int j = 0; j < maxActiveNpc / npcPrefabs.Length; j++)
            {
                GameObject cloneNpc = Instantiate(npcPrefabs[i]);
                cloneNpc.transform.parent = transform;
                cloneNpc.SetActive(false);
                currentNpcs.Add(cloneNpc);
            }
        }
    }

    float spawnCounter = 0;
    float[] xIndexs = { -2.25f, -7.75f, -12.75f, -17.75f, -22.75f, 2.25f, 7.75f, 12.75f, 17.75f, 22.75f  };
    void CallNpc()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter > spawnRate)
        {
            GameObject currentNPC = currentNpcs[0];
            if (!currentNPC.activeSelf)
            {
                spawnCounter = 0;
                spawnRate = Random.Range(0.5f, 2);

                float randomIndex = xIndexs[Random.Range(0, 10)];
                if (randomIndex > 0)
                {
                    currentNPC.GetComponent<BaseNpc>().zPositveDirection = true;
                    currentNPC.transform.rotation = Quaternion.Euler(0, 0, 0);
                    currentNPC.GetComponent<BaseNpc>().speed = 20;
                }
                else
                {
                    currentNPC.GetComponent<BaseNpc>().zPositveDirection = false;
                    currentNPC.transform.rotation = Quaternion.Euler(0, 180, 0);
                    currentNPC.GetComponent<BaseNpc>().speed = -20;
                }

                currentNPC.transform.position = new Vector3(randomIndex, 1, gameManager.PlayerTransform.position.z + spawnZ_Ofsset);
                currentNPC.SetActive(true);
                currentNpcs.Remove(currentNPC);
                currentNpcs.Add(currentNPC);
            }
            else
            {
                currentNpcs.Remove(currentNPC);
                currentNpcs.Add(currentNPC);
            }
        }
       
    }
}
