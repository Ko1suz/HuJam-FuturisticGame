using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightInAudio : MonoBehaviour
{
    public int band;
    public float minIntensity, maxIntensity;
    Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light= GetComponent<Light>();  
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = (AudioPeer.audioBandBuffer[band] * (maxIntensity - minIntensity)) + minIntensity;
    }
}
