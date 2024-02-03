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
    [HideInInspector] public List<TrailObject> _trails;
    [Header("Trail Properties")]
    public GameObject _trailPrefab;
    public AnimationCurve _trialWidhCurve;
    [Range(0,8)]
    public int _trailEndCapVertices;
    public Material _trailMaterial;
    public Gradient _trailColor;

    [Header("Audio")]
    public int[] _audioBand;
    public Vector2 _speedMinMax, _widhtMinMax, _trailTimeMinMax;
    public float _colorMultiplier;

    //Private varriables
    private float _lerpPosSpeed;
    private float _distanceSnap;
    private Color _startColor, _endColor;

    // Start is called before the first frame update
    void Start()
    {
        _startColor = new Color(0,0,0,0);
        _endColor = new Color(0,0,0,1);
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
            trailObjectInstance.Trail.widthCurve = _trialWidhCurve;

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
                    instantiatePositon = _postions[i * step];
                    trailObjectInstance.CurrentTargetNum = (i * step) + 1;
                    trailObjectInstance.TargetPositon = _postions[trailObjectInstance.CurrentTargetNum];
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

    void Movment()
    {
        _lerpPosSpeed = Mathf.Lerp(_speedMinMax.x, _speedMinMax.y, AudioPeer.amplitude);
        for (int i = 0; i < _trails.Count; i++)
        {
         
            _distanceSnap = Vector3.Distance(_trails[i].GO.transform.localPosition, _trails[i].TargetPositon);
            if (_distanceSnap < 0.05f)
            {
                _trails[i].GO.transform.localPosition = _trails[i].TargetPositon;
                if (_useBezierCurves && _generationCount > 0)
                {
                    if (_trails[i].CurrentTargetNum < _bezierPositons.Length - 1)
                    {
                        _trails[i].CurrentTargetNum += 1;
                    }
                    else
                    {
                        _trails[i].CurrentTargetNum = 1;
                    }
                    _trails[i].TargetPositon = _bezierPositons[_trails[i].CurrentTargetNum];
                }
                else
                {
                    if (_trails[i].CurrentTargetNum < _postions.Length - 1)
                    {
                        _trails[i].CurrentTargetNum += 1;
                    }
                    else
                    {
                        _trails[i].CurrentTargetNum = 1;
                    }
                    _trails[i].TargetPositon = _targetPostions[_trails[i].CurrentTargetNum];
                }
            }
            _trails[i].GO.transform.localPosition = Vector3.MoveTowards(_trails[i].GO.transform.localPosition, _trails[i].TargetPositon, Time.deltaTime * _lerpPosSpeed);
        }
    }
    void AudioBehaviour()
    {
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Color colorLerp = Color.Lerp(_startColor, _trails[i].EmissionColor * _colorMultiplier, AudioPeer.audioBand[_audioBand[i]]);
            _trails[i].Trail.material.SetColor("_HdrColor", colorLerp);

            colorLerp = Color.Lerp(_startColor, _endColor, AudioPeer.audioBand[_audioBand[i]]);
            _trails[i].Trail.material.SetColor("_MainColor", colorLerp);

            float widhtLerp = Mathf.Lerp(_widhtMinMax.x, _widhtMinMax.y, AudioPeer.audioBandBuffer[_audioBand[i]]);
            _trails[i].Trail.widthMultiplier = widhtLerp;

            float timeLerp = Mathf.Lerp(_trailTimeMinMax.x, _trailTimeMinMax.y, AudioPeer.audioBandBuffer[_audioBand[i]]);
            _trails[i].Trail.time = timeLerp;

        }
    }
    // Update is called once per frame
    void Update()
    {
        Movment();
        AudioBehaviour();
    }
}
