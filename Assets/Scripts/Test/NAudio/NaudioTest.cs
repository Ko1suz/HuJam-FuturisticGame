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
        // NAudio kayd� ba�lat
        waveIn = new WasapiLoopbackCapture();
        waveIn.DataAvailable += OnDataAvailable;
        waveIn.StartRecording();
    }

    void Update()
    {
        // Her g�ncelleme d�ng�s�nde ses verilerini i�leyin
        if (waveIn != null && waveIn.CaptureState == NAudio.CoreAudioApi.CaptureState.Capturing)
        {
            //byte[] buffer = new byte[1024]; // �rnek: 1024 byte'l�k bir veri okuma
            //int bytesRead = waveIn.Read(buffer, 0, buffer.Length);

            //// Ses verilerini i�leyin
            //// �rnek: Debug.Log ile ses verilerini yazd�rma
            //Debug.Log("Ses Verisi Al�nd�. Okunan Byte Say�s�: " + bytesRead);
        }
    }

    void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        // Burada da ses verilerini i�leyebilirsiniz, ancak Update fonksiyonu bu i�i s�rekli olarak yapar.
    }

    void OnApplicationQuit()
    {
        // Uygulama sona erdi�inde NAudio kayd�n� durdur
        if (waveIn != null)
        {
            waveIn.StopRecording();
            waveIn.Dispose();
        }
    }
}
