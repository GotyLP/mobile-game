using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HuntingState : IState
{
    FSM<string> _fsm;
    private float _radiusPersuit;
    private float _radiusAttack;
    Transform _transform;
    private Player _currentTarget;

    private Func<Player, Vector3> _pursuitFunc;
    private Action<Vector3> _addForceFunc;
    private Action<Player> _attackFunc;
    private Func<float> _getEnergy;
    private Action<float> _setEnergy;
    private Action<Vector3> _setVelocity;

    public HuntingState(
        FSM<string> fsm, 
        Transform transform, 
        float radiusPersuit, 
        float radiusAttack, 
        Func<Player, Vector3> Persuit, 
        Action<Vector3> addForce, 
        Action<Player> attackFunc,
        Func<float> getEnergy,
        Action<float> setEnergy,
        Action<Vector3> setVelocity
        )
    {
        _fsm = fsm;
        _transform = transform;
        _radiusPersuit = radiusPersuit;
        _radiusAttack = radiusAttack;
        _pursuitFunc = Persuit;
        _addForceFunc = addForce;
        _attackFunc = attackFunc;
        _getEnergy = getEnergy;
        _setEnergy = setEnergy;
        _setVelocity = setVelocity;
    }
    public void OnEnter()
    {
        Debug.Log("On Enter HuntingState");
        _currentTarget = null;
    }

    public void OnExit()
    {
        Debug.Log("On Exit HuntingState");
        _currentTarget = null;
    }

    private void FindTarget(Player target)
    {

        if (target == null) return;
        float dist = Vector3.Distance(target.transform.position, _transform.position);

        if (dist < _radiusAttack || dist < _radiusPersuit)
        {
            _currentTarget = target;
            return;
        }

        _fsm.ChangeState(EnemyDistanceStatesNames.Movement);
    }


    public void PersuitTarget(Player target)
    {
        Vector3 steering = _pursuitFunc(target);
        _addForceFunc?.Invoke(steering);
    }

    public void AttackTarget(Player target)
    {
        _attackFunc?.Invoke(target);
        _setVelocity(Vector3.zero);
    }

    public void OnUpdate()
    {
        Debug.Log("OnUpdate HuntingState - Energía antes: " + _getEnergy());
        float currentEnergy = _getEnergy();
        currentEnergy -= Time.deltaTime;
        _setEnergy(currentEnergy);
        Debug.Log("OnUpdate HuntingState - Energía después: " + _getEnergy());
        if (currentEnergy <= 0)
            _fsm?.ChangeState(EnemyDistanceStatesNames.Idle);

        if (_currentTarget != null)
        {
            float dist = Vector3.Distance(_currentTarget.transform.position, _transform.position);

            if (dist < _radiusAttack)
            {
                AttackTarget(_currentTarget);
            }
            else if (dist < _radiusPersuit)
            {
                PersuitTarget(_currentTarget);
            }
            else
            {
                _currentTarget = null;
                _fsm.ChangeState(EnemyDistanceStatesNames.Movement);
            }
        } else
        {
            FindTarget(GameManager.Instance.player);
        }

    }
}
