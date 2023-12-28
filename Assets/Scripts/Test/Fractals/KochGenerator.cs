using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour
{
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
    [SerializeField] private float _initiatorSize;

   
    public void OnDrawGizmos()
    {
        GetInitiatorPoints();
        Gizmos.color = Color.green;
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        _rotateVector = new Vector3(0,0,1);
        _rotateAxis = new Vector3(0,1, 0);
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
                break;
            case _initiator.Sqare:
                _initiatorPointAmount = 4;
                break;
            case _initiator.Pentagon:
                _initiatorPointAmount = 5;
                break;
            case _initiator.Hexagon:
                _initiatorPointAmount = 6;
                break;
            case _initiator.Heptagon:
                _initiatorPointAmount = 7;
                break;
            case _initiator.Octagon:
                _initiatorPointAmount = 8;
                break;
            default:
                _initiatorPointAmount = 3;
                break;
        }
    }
}
