using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFControler : MonoBehaviour
{
    #region Exposed

    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _endPoint;
    [SerializeField] float _timeToReach;

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        transform.position = _startPoint.position;
    }

    void Update()
    {
        if (isForward)
        {
            _timerMovement += Time.deltaTime;
            if (_timerMovement >= _timeToReach)
            {
                isForward = false;
            }
        }
        else
        {
            _timerMovement -= Time.deltaTime;
            if (_timerMovement <= 0f)
            {
                isForward = true;
            }
        }

        Vector3 newPosition =  Vector3.Lerp(_startPoint.position, _endPoint.position, _timerMovement / _timeToReach);

        transform.position = newPosition;
    }

    #endregion

    #region Methods



    #endregion

    #region Private & Protected

    private float _timerMovement;
    private bool isForward = true;

    #endregion
}
