using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolumeSync : MonoBehaviour
{
    GameManager gameManager;
    PostProccesControl postProccesControl;
    public int channel = 0;
    public float bloomMulti = 25;
    public float lensDistortionValue = -0.1f;
    public float fovMulti = 25;
    public bool fovSettings = true;
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
        postProccesControl.lensDistortion.intensity.value = Mathf.Lerp(0, -lensDistortionValue, AudioPeer.audioBandBuffer[channel]);

        if (fovSettings)
        {
            if ((AudioPeer.audioBandBuffer[channel]) > 0.55f)
            {
                postProccesControl.mainCamera.fieldOfView = Mathf.Lerp(postProccesControl.mainCamera.fieldOfView, 60 + (fovMulti * (AudioPeer.audioBandBuffer[channel])), 0.1f); ; ;
            }
            else
            {
                postProccesControl.mainCamera.fieldOfView = Mathf.Lerp(postProccesControl.mainCamera.fieldOfView, 60, 0.1f); ;
            }
        }
       
    }
}
