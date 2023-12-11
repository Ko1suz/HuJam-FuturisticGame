using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionControl : MonoBehaviour
{
    public List<BaseDimension> dimensionPrefabs;
    GameManager gameManager;

    [Header("CurrentValue")]
    public List<GameObject> currentDimensions;
    public int dimensionIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
        CreateDimenions();
        gameManager.PlayerTransform.position = Vector3.zero;
        BaseDimension.current_Z_pos = 0;
        currentDimensions[dimensionIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDimension();
    }


    void CreateDimenions()
    {
        for (int i = 0; i < dimensionPrefabs.Capacity; i++)
        {
            GameObject cloneDimension = Instantiate(dimensionPrefabs[i].gameObject);
            cloneDimension.SetActive(false);
            cloneDimension.GetComponent<BaseDimension>().SpawnParts();
            currentDimensions.Add(cloneDimension);
        }
    }

    void ChangeDimension()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (dimensionIndex + 1  < currentDimensions.Count)  { dimensionIndex++; }
            else{ dimensionIndex = 0; }

            for (int i = 0; i < currentDimensions.Count; i++)
            {
                currentDimensions[i].SetActive(false);
            }
            gameManager.PlayerTransform.position = Vector3.zero;
            BaseDimension.current_Z_pos = 0;
            currentDimensions[dimensionIndex].SetActive(true);
        }
    }

    public AudioClip ReturnDimensionSong()
    {
        return currentDimensions[dimensionIndex].GetComponent<BaseDimension>().dimensionMusics[Random.Range(0, currentDimensions[dimensionIndex].GetComponent<BaseDimension>().dimensionMusics.Length)];
    }
}
