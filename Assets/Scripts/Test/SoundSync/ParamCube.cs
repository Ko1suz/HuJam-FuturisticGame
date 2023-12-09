using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int band;
    public float startScale, scaleMulti;
    public bool useBuffer;
    Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }
    void Update()
    {
        if (useBuffer)
        {
            transform.localScale = new Vector3(10, (AudioPeer.audioBandBuffer[band] * scaleMulti) + startScale, 10);
            Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBand[band], 0);
            material.SetColor("_MainColor", _color);
            //Debug.Log(AudioPeer.audioBandBuffer[band]);
        }
        else
        {
            transform.localScale = new Vector3(10, (AudioPeer.audioBand[band] * scaleMulti) + startScale, 10);
            Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
            material.SetColor("_MainColor", _color);
        }
       
    }
}
