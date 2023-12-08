using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int band;
    public float startScale, scaleMulti;
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.freqBand[band] * scaleMulti) + startScale, transform.localScale.z  );
    }
}
