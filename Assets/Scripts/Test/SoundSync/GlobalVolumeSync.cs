using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolumeSync : MonoBehaviour
{
    GameManager gameManager;
    PostProccesControl postProccesControl;
    public int channel = 0;
    public float bloomMulti = 25;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager._instance;
        postProccesControl = gameManager.PostProccesControl;
    }

    // Update is called once per frame
    void Update()
    {
        IntesitySync();
    }

    void IntesitySync()
    {
        postProccesControl.bloom.intensity.value = bloomMulti * (AudioPeer.audioBandBuffer[channel]);
        //postProccesControl.lensDistortion.intensity.value = Mathf.Lerp(0.4f, -0.4f, AudioPeer.audioBandBuffer[channel]);
    }
}
