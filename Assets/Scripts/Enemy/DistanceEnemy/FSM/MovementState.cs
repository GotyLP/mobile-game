using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : IState
{
    Transform _transform;
    FSM<string> _fsm;
    private float _radiusPersuit;
    [SerializeField] private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;
    Func<Vector3, Vector3> _seekFunc;
    Action<Vector3> _addForce;
    Func<float> _getEnergy;
    Action<float> _setEnergy;

    public MovementState(
          FSM<string> fsm,
          Transform transform,
          float radiusPersuit,
          Transform[] waypoints,
          Func<Vector3, Vector3> seekFunc,
          Action<Vector3> addForceFunc,
          Func<float> getEnergy,
          Action<float> setEnergy
    )
    {
        _fsm = fsm;
        _transform = transform;
        _waypoints = waypoints;
        _seekFunc = seekFunc;
        _addForce = addForceFunc;
        _radiusPersuit = radiusPersuit;
        _getEnergy = getEnergy;
        _setEnergy = setEnergy;
    }

    public void OnEnter()
    {
        Debug.Log("OnEnter MovementState");
    }

    public void OnUpdate()
    {
        Debug.Log("OnUpdate MovementState");        

        if (_waypoints.Length == 0) return;

        Transform currentTarget = _waypoints[_currentWaypointIndex];
        Vector3 steering = _seekFunc(currentTarget.position);
        _addForce?.Invoke(steering);
        Debug.DrawLine(_transform.position, currentTarget.position, Color.red);
        float distance = Vector3.Distance(_transform.position, currentTarget.position);

        if (distance < 1f) 
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        }

        float currentEnergy = _getEnergy();
        currentEnergy -= Time.deltaTime;
        _setEnergy(currentEnergy);

        if (currentEnergy <= 0)
            _fsm?.ChangeState(EnemyDistanceStatesNames.Idle);

        Player player = GameManager.Instance.player;
        if (player != null && Vector3.Distance(player.transform.position, _transform.position) < _radiusPersuit)
        {
            _fsm.ChangeState(EnemyDistanceStatesNames.Hunting);
        }
    }

    public void OnExit()
    {
        Debug.Log("OnExit MovementState");
    }

}
