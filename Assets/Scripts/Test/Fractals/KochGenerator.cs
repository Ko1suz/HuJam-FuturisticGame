using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour
{
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    };
    [SerializeField] protected _axis axis = new _axis();
    protected enum _initiator
    {
        Triangle,
        Sqare,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon
    }
    public struct LineSegment
    {
        public Vector3 StartPostion { get; set; }
        public Vector3 EndPostion { get; set; }
        public Vector3 Direction { get; set; }
        public float Length { get; set; }
    }


    [SerializeField] protected _initiator initiator = new _initiator();

    [SerializeField] protected AnimationCurve _generator;
    [SerializeField] protected Keyframe[] _keys;
    protected int _generationCount;

    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initalRotation;
    [SerializeField] private float _initiatorSize;

    protected Vector3[] _postions;
    protected Vector3[] _targetPostions;
    private List<LineSegment> _lineSegmets;
    private void Awake()
    {
        GetInitiatorPoints();
        // assign list & arrays
        Gizmos.color = Color.green;
        _postions = new Vector3[_initiatorPointAmount + 1];
        _targetPostions = new Vector3[_initiatorPointAmount + 1];
        _lineSegmets = new List<LineSegment>();
        _keys = _generator.keys;

        _rotateVector = Quaternion.AngleAxis(_initalRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _postions[i] = _rotateVector * _initiatorSize; 
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _postions[_initiatorPointAmount] = _postions[0];
        _targetPostions = _postions;
    }
    protected void KochGenerate(Vector3[] postions, bool outwards, float generatorMultiplayer)
    {
        //creating line segments
        _lineSegmets.Clear();
        for (int i = 0; i < postions.Length - 1; i++)
        {
            LineSegment line = new LineSegment();
            line.StartPostion = postions[i];
            if (i == postions.Length - 1)
            {
                line.EndPostion = postions[0];
            }
            else
            {
                line.EndPostion = postions[i + 1];
            }
            line.Direction = (line.EndPostion - line.StartPostion).normalized;
            line.Length = Vector3.Distance(line.EndPostion, line.StartPostion);
            _lineSegmets.Add(line);
        }
        // add the line segment points to a point array
        List<Vector3> newPos = new List<Vector3>();
        List<Vector3> targetPos = new List<Vector3>();

        for (int i = 0; i < _lineSegmets.Count; i++)
        {
            newPos.Add(_lineSegmets[i].StartPostion);
            targetPos.Add(_lineSegmets[i].StartPostion);
            for (int j = 0; j < _keys.Length - 1; j++)
            {
                float moveAmount = _lineSegmets[i].Length * _keys[j].time;
                float hightAmount = _lineSegmets[i].Length * _keys[j].value * generatorMultiplayer;
                Vector3 movePos = _lineSegmets[i].StartPostion + (_lineSegmets[i].Direction * moveAmount);
                Vector3 Direction;
                if (outwards)
                {
                    Direction = Quaternion.AngleAxis(-90, _rotateAxis) * _lineSegmets[i].Direction;
                }
                else
                {
                    Direction = Quaternion.AngleAxis(90, _rotateAxis) * _lineSegmets[i].Direction;
                }
                newPos.Add(movePos);
                targetPos.Add(movePos + (Direction * hightAmount));
                
            }
        }
        newPos.Add(_lineSegmets[0].StartPostion);
        targetPos.Add(_lineSegmets[0].StartPostion);
        _postions = new Vector3[newPos.Count];
        _targetPostions = new Vector3[targetPos.Count];
        _postions = newPos.ToArray();
        _targetPostions = targetPos.ToArray();


        _generationCount++;
    }
    public void OnDrawGizmos()
    {
        GetInitiatorPoints();
        Gizmos.color = Color.green;
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        _rotateVector = Quaternion.AngleAxis(_initalRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Matrix4x4 rotatinMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotatinMatrix;
            if (i < _initiatorPointAmount - 1)
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]);
            }
        }
    }
    private void GetInitiatorPoints()
    {
        switch (initiator)
        {
            case _initiator.Triangle:
                _initiatorPointAmount = 3;
                _initalRotation = 0;
                break;
            case _initiator.Sqare:
                _initiatorPointAmount = 4;
                _initalRotation = 45;
                break;
            case _initiator.Pentagon:
                _initiatorPointAmount = 5;
                _initalRotation = 36;
                break;
            case _initiator.Hexagon:
                _initiatorPointAmount = 6;
                _initalRotation = 30;
                break;
            case _initiator.Heptagon:
                _initiatorPointAmount = 7;
                _initalRotation = 25.7148f;
                break;
            case _initiator.Octagon:
                _initiatorPointAmount = 8;
                _initalRotation = 22.5f;
                break;
            default:
                _initiatorPointAmount = 3;
                _initalRotation = 0;
                break;
        }

        switch (axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1, 0, 0);
                _rotateAxis = new Vector3(0, 0, 1);
                break;
            case _axis.YAxis:

                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;
            case _axis.ZAxis:

                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0);
                break;
            default:

                _rotateVector = new Vector3(1, 0, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;
        }
    }
}
