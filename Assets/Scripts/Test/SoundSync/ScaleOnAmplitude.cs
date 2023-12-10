using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public float startScale, maxScale;
    public bool useBuffer;
    Material material;
    public float red, green, blue;  

    public bool scaleUp = false;
    public float scaleMin = 1;
    public float scaleMax = 200;
    // Start is called before the first frame update
    void Start()
    {
        material= GetComponent<MeshRenderer>().material; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer.amplitude * maxScale) + startScale, (AudioPeer.amplitude * maxScale) + startScale, (AudioPeer.amplitude * maxScale) + startScale);
            float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
            material.SetFloat("_Intesity", intensity);
        }
        else
        {
            transform.localScale = new Vector3((AudioPeer.amplitudeBuffer * maxScale) + startScale, (AudioPeer.amplitudeBuffer * maxScale) + startScale, (AudioPeer.amplitudeBuffer * maxScale) + startScale);
            float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
            material.SetFloat("_Intesity", intensity);
        }
        ScaleUp();
    }

    float timer;
    void ScaleUp()
    {
        if (scaleUp && timer <= 60)
        {
            this.gameObject.transform.localScale = Vector3.Lerp(new Vector3(scaleMin, scaleMin, scaleMin ), new Vector3(scaleMax, scaleMax, scaleMax) , timer/60);
            timer += Time.deltaTime;
        }
    }
}
