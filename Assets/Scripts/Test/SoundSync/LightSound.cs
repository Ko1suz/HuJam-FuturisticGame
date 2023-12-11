using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSound : MonoBehaviour
{
    Material material;
    [Range(0, 7)]
    public int channel = 1;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        float intensity = 10 * (AudioPeer.audioBandBuffer[channel] * 10);
        material.SetFloat("_Intesity", intensity);
    }
}
