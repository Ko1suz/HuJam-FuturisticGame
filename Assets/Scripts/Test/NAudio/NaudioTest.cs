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
            // Ses verilerini i�leyin
            byte[] buffer = new byte[e.BytesRecorded];
            Buffer.BlockCopy(e.Buffer, 0, buffer, 0, e.BytesRecorded);

            // Unity i�inde bu sesi kullanmak i�in gerekli i�lemleri ger�ekle�tirin
            // �rne�in, bu sesi bir ses nesnesine ba�lamak i�in Unity API'lerini kullan�n.
        };
        waveIn.StartRecording();

        Debug.Log("Ses dinleniyor. ��kmak i�in bir tu�a bas�n.");

        //waveIn.StopRecording();
    }
}
