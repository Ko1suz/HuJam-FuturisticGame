using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;


public class NaudioTest : MonoBehaviour
{
    private WasapiLoopbackCapture waveIn;

    void Start()
    {
        // NAudio kaydý baþlat
        waveIn = new WasapiLoopbackCapture();
        waveIn.DataAvailable += OnDataAvailable;
        waveIn.StartRecording();
    }

    void Update()
    {
        // Her güncelleme döngüsünde ses verilerini iþleyin
        if (waveIn != null && waveIn.CaptureState == NAudio.CoreAudioApi.CaptureState.Capturing)
        {
            //byte[] buffer = new byte[1024]; // Örnek: 1024 byte'lýk bir veri okuma
            //int bytesRead = waveIn.Read(buffer, 0, buffer.Length);

            //// Ses verilerini iþleyin
            //// Örnek: Debug.Log ile ses verilerini yazdýrma
            //Debug.Log("Ses Verisi Alýndý. Okunan Byte Sayýsý: " + bytesRead);
        }
    }

    void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        // Burada da ses verilerini iþleyebilirsiniz, ancak Update fonksiyonu bu iþi sürekli olarak yapar.
    }

    void OnApplicationQuit()
    {
        // Uygulama sona erdiðinde NAudio kaydýný durdur
        if (waveIn != null)
        {
            waveIn.StopRecording();
            waveIn.Dispose();
        }
    }
}
