using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator
{
    LineRenderer _lineRenderer;
    //[Range(0,1)]
    //public float _lerpAmount;
    Vector3[] _lerpPostions;
    public float _generateMultiplier;
    private float[] _lerpAudio;

    [Header("Audio")]
    public AudioPeer _audioPeer;
    public int[] _audioBands;
    // Start is called before the first frame update
    void Start()
    {
        _lerpAudio = new float[_initiatorPointAmount];
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _postions.Length;
        _lineRenderer.SetPositions(_postions);
        _lerpPostions = new Vector3[_postions.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (_generationCount != 0)
        {
            int count = 0;
            for (int i = 0; i < _initiatorPointAmount; i++)
            {
                _lerpAudio[i] = AudioPeer.audioBandBuffer[_audioBands[i]];
                for (int j = 0; j < (_postions.Length - 1) / _initiatorPointAmount; j++)
                {
                    _lerpPostions[count] = Vector3.Lerp(_postions[count], _targetPostions[count], _lerpAudio[i]);
                    count++;
                }
            }
            _lerpPostions[count] = Vector3.Lerp(_postions[count], _targetPostions[count], _lerpAudio[_initiatorPointAmount - 1]);
            if (_useBezierCurves)
            {
                _bezierPositons = BezierCurve(_lerpPostions, _bezierVertexCount);
                _lineRenderer.positionCount = _bezierPositons.Length;
                _lineRenderer.SetPositions(_bezierPositons);
            }
            else
            {
                _lineRenderer.positionCount = _lerpPostions.Length;
                _lineRenderer.SetPositions(_lerpPostions);
            }
          
        }
    }
}
