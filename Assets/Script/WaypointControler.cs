using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WaypointMode
{
    LOOP,
    PINGPONG
}

public class WaypointControler : MonoBehaviour
{
    #region Exposed

    [SerializeField] Transform[] _waypoint;
    [SerializeField] float _speed;
    [SerializeField] float _distTolerance;
    [SerializeField] WaypointMode _mode = WaypointMode.LOOP;

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        /*
        int startWaypoint = Random.Range(0, _waypoint.Length -1);
        transform.position = _waypoint[startWaypoint].position;
        _targetWaypoint = startWaypoint + 1;
        */

        transform.position = _waypoint[0].position;

        _targetWaypoint = 1;

    }

    void Update()
    {

        Vector3 newPosition = Vector3.MoveTowards(transform.position, _waypoint[_targetWaypoint].position, _speed * Time.deltaTime);

        transform.position = newPosition;

        if (Vector3.Distance(transform.position, _waypoint[_targetWaypoint].position) <= _distTolerance)
        {
            switch (_mode)
            {
                case WaypointMode.LOOP:
                    Loop();
                    break;
                case WaypointMode.PINGPONG:
                    Pingpong();
                    break;
            }
        }

    }

    #endregion

    #region Methods

    void Loop()
    {
        _targetWaypoint++;
        if (_targetWaypoint >= _waypoint.Length)
        {
            _targetWaypoint = 0;
        }
    }

    void Pingpong()
    {
        if (_isForward)
        {
            _targetWaypoint++;
            if (_targetWaypoint >= _waypoint.Length - 1)
            {
                _isForward = false;
            }
        }
        else
        {
            _targetWaypoint--;
            if (_targetWaypoint <= 0)
            {
                _isForward = true;
            }
        }
    }

    #endregion

    #region Private & Protected

    private int _targetWaypoint;
    private bool _isForward = true;

    #endregion
}
