using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochTrail : KochGenerator
{
    public class TrailObject 
    {
        public GameObject GO {  get; set; }
        public TrailRenderer Trail { get; set; }
        public int CurrentTargetNum { get; set; }
        public Vector3 TargetPositon { get; set; }
        public Color EmissionColor { get; set; }
    }
    public List<TrailObject> _trails;
    [Header("Trail Properties")]
    public GameObject _trailPrefab;
    public AnimationCurve _trialWidhCurve;
    [Range(0,8)]
    public int _trailEndCapVertices;
    public Material _trailMaterial;
    public Gradient _trailColor;

    [Header("Audio")]
    public int[] _audioBand;
    // Start is called before the first frame update
    void Start()
    {
        _trails = new List<TrailObject>();
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            GameObject trailInstance = (GameObject)Instantiate(_trailPrefab, transform.position, Quaternion.identity, this.transform);
            TrailObject trailObjectInstance = new TrailObject();
            trailObjectInstance.GO = trailInstance;
            trailObjectInstance.Trail = trailInstance.GetComponent<TrailRenderer>();
            trailObjectInstance.Trail.material = new Material(_trailMaterial);
            trailObjectInstance.EmissionColor = _trailColor.Evaluate(i * (1.0f / _initiatorPointAmount));
            trailObjectInstance.Trail.numCapVertices = _trailEndCapVertices;

            Vector3 instantiatePositon;
            //Instantiate

            if (_generationCount > 0) 
            {
                int step;
                if (_useBezierCurves)
                {
                    step = _bezierPositons.Length / _initiatorPointAmount;
                    instantiatePositon = _bezierPositons[i * step];
                    trailObjectInstance.CurrentTargetNum = (i * step) + 1;
                    trailObjectInstance.TargetPositon = _bezierPositons[trailObjectInstance.CurrentTargetNum];
                }
                else
                {
                    step = _postions.Length / _initiatorPointAmount;
                    instantiatePositon = _bezierPositons[i * step];
                    trailObjectInstance.CurrentTargetNum = (i * step) + 1;
                }
            }
            else
            {
                instantiatePositon = _postions[i];
                trailObjectInstance.CurrentTargetNum = i + 1;
                trailObjectInstance.TargetPositon = _postions[trailObjectInstance.CurrentTargetNum];

            }
            trailObjectInstance.GO.transform.localPosition = instantiatePositon;
            _trails.Add(trailObjectInstance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
