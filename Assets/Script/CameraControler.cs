using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    #region Exposed

    [SerializeField] Transform _target;
    [SerializeField] float _lerpTime = 5f;

    #endregion

    #region Unity Lifecycle

    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        _velocity = Vector3.zero;

        Vector3 targetPosition_zOffset = new Vector3(_target.position.x, _target.position.y, -10);

        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition_zOffset, ref _velocity, _lerpTime);

        transform.position = newPosition;
        //transform.position = new Vector3(_target.position.x, _target.position.y, -10f);
    }

    #endregion

    #region Methods



    #endregion

    #region Private & Protected

    private Vector3 _velocity;

    #endregion
}
