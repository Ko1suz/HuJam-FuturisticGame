using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance512Cubes : MonoBehaviour
{
    public GameObject sampleCubePrefab;
    GameObject[] sampleCube = new GameObject[512];
    public float multiplyHeight = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject cloneCube = Instantiate(sampleCubePrefab);
            cloneCube.transform.position = this.transform.position;
            cloneCube.transform.parent = this.transform;
            cloneCube.name = "CloneKüpId -> " + i;
            this.transform.eulerAngles = new Vector3(0.0f, -0.703125f * i, 0.0f);
            cloneCube.transform.position = Vector3.forward * 100;
            sampleCube[i] = cloneCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (sampleCube != null)
            {
                sampleCube[i].transform.localScale = new Vector3(10, (AudioPeer.samples[i] * multiplyHeight) + 2, 10);
            }
        }
    }
}
