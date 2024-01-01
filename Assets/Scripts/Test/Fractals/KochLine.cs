using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class KochLine : KochGenerator
{
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();    
        lineRenderer.positionCount = _postions.Length;
        lineRenderer.SetPositions(_postions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
