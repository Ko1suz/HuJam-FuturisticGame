using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxSoundSync : MonoBehaviour
{
    public Transform[] transforms;
    public int[] bands;
    public float multiply;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        SetPostions();
    }

    void SetPostions()
    {
        for (int i = 0; i < 4; i++)
        {
            transforms[i].transform.position = new Vector3(transforms[i].transform.position.x, AudioPeer.audioBandBuffer[bands[i]] * multiply, transforms[i].transform.position.z) ;
        }
    }
}
