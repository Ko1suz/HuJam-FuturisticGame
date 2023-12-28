using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;


public class NaudioTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SoundListener();
    }

    void SoundListener()
    {
        var waveIn = new WasapiLoopbackCapture();
        waveIn.DataAvailable += (sender, e) =>
        {
            // Ses verilerini iþleyin
            byte[] buffer = new byte[e.BytesRecorded];
            Buffer.BlockCopy(e.Buffer, 0, buffer, 0, e.BytesRecorded);

            // Unity içinde bu sesi kullanmak için gerekli iþlemleri gerçekleþtirin
            // Örneðin, bu sesi bir ses nesnesine baðlamak için Unity API'lerini kullanýn.
        };
        waveIn.StartRecording();

        Debug.Log("Ses dinleniyor. Çýkmak için bir tuþa basýn.");

        //waveIn.StopRecording();
    }
}
