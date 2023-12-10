using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int band;
    public float startScale, scaleMulti;
    public bool useBuffer;
    Material material;

    public bool scaleX = false;
    public bool scaleY = true;
    public bool scaleZ = false;
    private void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }
    void LateUpdate()
    {
        ScaleX();
        ScaleY();
        ScaleZ();
       
    }

    void ScaleX()
    {
        if (scaleX)
        {
            if (useBuffer)
            {
                transform.localScale = new Vector3((AudioPeer.audioBandBuffer[band] * scaleMulti) + startScale, transform.localScale.y, transform.localScale.z);
                Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBand[band], 0);
                material.SetColor("_MainColor", _color);
                float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                material.SetFloat("_Intesity", intensity);
                //Debug.Log(AudioPeer.audioBandBuffer[band]);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBand[band] * scaleMulti) + startScale, transform.localScale.z);
                Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
                material.SetColor("_MainColor", _color);
                float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                material.SetFloat("_Intesity", intensity);
            }
        }
     
    }
    void ScaleY()
    {
        if (scaleY)
        {
            if (useBuffer)
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBandBuffer[band] * scaleMulti) + startScale, transform.localScale.z);
                Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBand[band], 0);
                material.SetColor("_MainColor", _color);
                float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                material.SetFloat("_Intesity", intensity);
                //Debug.Log(AudioPeer.audioBandBuffer[band]);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBand[band] * scaleMulti) + startScale, transform.localScale.z);
                Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
                material.SetColor("_MainColor", _color);
                float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                material.SetFloat("_Intesity", intensity);
            }
        }
    }
    void ScaleZ()
    {
        if (scaleZ)
        {
            if (useBuffer)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, (AudioPeer.audioBandBuffer[band] * scaleMulti) + startScale);
                Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBand[band], 0);
                material.SetColor("_MainColor", _color);
                float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                material.SetFloat("_Intesity", intensity);
                //Debug.Log(AudioPeer.audioBandBuffer[band]);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBand[band] * scaleMulti) + startScale, transform.localScale.z);
                Color _color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
                material.SetColor("_MainColor", _color);
                float intensity = 10 * (AudioPeer.audioBandBuffer[1] * 5);
                material.SetFloat("_Intesity", intensity);
            }
        }
    }
}
