using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator
{
    LineRenderer _lineRenderer;
    [Range(0,1)]
    public float _lerpAmount;
    Vector3[] _lerpPostions;
    public float _generateMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _postions.Length;
        _lineRenderer.SetPositions(_postions);
    }

    // Update is called once per frame
    void Update()
    {
        if (_generationCount != 0)
        {
            for (int i = 0; i < _postions.Length; i++)
            {
                _lerpPostions[i] = Vector3.Lerp(_postions[i], _targetPostions[i], _lerpAmount);
            }
            _lineRenderer.SetPositions(_lerpPostions);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            KochGenerate(_targetPostions, true, _generateMultiplier);
            _lerpPostions = new Vector3[_postions.Length];
            _lineRenderer.positionCount = _postions.Length;
            _lineRenderer.SetPositions(_postions);
            _lerpAmount = 0;
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            KochGenerate(_targetPostions, false, _generateMultiplier);
            _lerpPostions = new Vector3[_postions.Length];
            _lineRenderer.positionCount = _postions.Length;
            _lineRenderer.SetPositions(_postions);
            _lerpAmount = 0;
        }
    }
}
