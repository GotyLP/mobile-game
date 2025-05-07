using System;
using UnityEngine;

public class IdleState : IState
{
    FSM<string> _fsm;
    Func<float> _getEnergy;
    Action<float> _setEnergy;
    float _maxEnergy;
    float _regenSpeed;
    Action<Vector3> _setVelocity;

    public IdleState(FSM<string> fsm, Func<float> getEnergy, Action<float> setEnergy, float maxEnergy, float regenSpeed, Action<Vector3> setVelocity)
    {
        _fsm = fsm;
        _getEnergy = getEnergy;
        _setEnergy = setEnergy;
        _maxEnergy = maxEnergy;
        _regenSpeed = regenSpeed;
        _setVelocity = setVelocity;
    }

    public void OnEnter()
    {
        Debug.Log("OnEnter IdleState");
        _setVelocity?.Invoke(Vector3.zero);
    }

    public void OnUpdate()
    {
        Debug.Log("OnUpdate IdleState");
        float currentEnergy = _getEnergy();
        currentEnergy += _regenSpeed * Time.deltaTime;

        _setEnergy(currentEnergy);

        if (currentEnergy >= _maxEnergy)
        {
            _fsm.ChangeState(EnemyDistanceStatesNames.Movement);
        }
    }

    public void OnExit()
    {
        Debug.Log("OnExit IdleState");
    }
}
