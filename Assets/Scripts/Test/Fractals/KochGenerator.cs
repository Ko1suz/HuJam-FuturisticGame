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
    [SerializeField] protected _initiator initiator = new _initiator();

    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initalRotation;
    [SerializeField] private float _initiatorSize;

    protected Vector3[] _postions;
    private void Awake()
    {
        GetInitiatorPoints();
        Gizmos.color = Color.green;
        _postions = new Vector3[_initiatorPointAmount + 1];

        _rotateVector = Quaternion.AngleAxis(_initalRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _postions[i] = _rotateVector * _initiatorSize; 
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _postions[_initiatorPointAmount] = _postions[0];
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
