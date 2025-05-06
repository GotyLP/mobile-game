using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DistanceEnemy : MonoBehaviour, IProduct
{    
    [SerializeField] float _speedMov;
    [SerializeField] float _speedReg;
    [SerializeField] float _energy;
    [SerializeField] float _maxEnergy;
    FSM<string> _fsm;
    [SerializeField] Vector3 _velocity;
    [SerializeField] float _maxVelocity;
    [SerializeField] float _radiusPersuit;
    [SerializeField] float _radiusAttack;
    [SerializeField] float _maxForce;
    [SerializeField] float _prediction;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _firePoint;
    [SerializeField] private float _shootCooldown = 1f;
    private float _lastShootTime = -Mathf.Infinity;

    [SerializeField] private Transform[] _waypoints;


    public Vector3 Velocity => _velocity;

    public void Initialize()
    {
        Debug.Log("Enemy Initialized");
    }

    private void Awake()
    {

        _fsm = new FSM<string>();

        _fsm.AddState(EnemyDistanceStatesNames.Idle, new IdleState(
             _fsm,
             () => _energy,
             (value) => _energy = value,
             _maxEnergy,
             _speedReg
         ));
        _fsm.AddState(EnemyDistanceStatesNames.Movement, new MovementState(
            _fsm,
            transform,
            _radiusPersuit,
            _waypoints,
            (targetPos) => Seek(targetPos),
            (force) => AddForce(force),
             () => _energy,
            (value) => _energy = value
        ));
        _fsm.AddState(EnemyDistanceStatesNames.Hunting, new HuntingState(
            _fsm,
            transform,
            _radiusPersuit,
            _radiusAttack,
            (target) => Pursuit(target),
            (force) => AddForce(force),
            (target) => ShootAtTarget(target),
            () => _energy,
            (value) => _energy = value
            )
        );

        _fsm.ChangeState(EnemyDistanceStatesNames.Movement);
    }

    private void Update()
    {
        _fsm.ArtificialUpdate();

        if (_fsm.CurrentStateKey == EnemyDistanceStatesNames.Idle) return;
        transform.position += _velocity * Time.deltaTime;
    }

    public void AddForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);

        if (_velocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(_velocity.x, 0, _velocity.z);
            if (direction.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);


        return steering;
    }

    public Vector3 Pursuit(Player target)
    {
        if (target == null) return Vector3.zero;

        Vector3 futurePosition = target.transform.position;
        return Seek(futurePosition);
    }


    public void ShootAtTarget(Player target)
    {
        if (Time.time - _lastShootTime < _shootCooldown)
            return;

        _lastShootTime = Time.time;

        if (target == null) return;

        BulletEnemy bullet = BulletEnemyFactory.Instance.GetFromPool();
        if (bullet == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;

        bullet.transform.position = transform.position;
        bullet.SetDirection(direction);
    }
}

public static class EnemyDistanceStatesNames
{
    public static string Idle = "Idle";
    public static string Movement = "Movement";
    public static string Hunting = "Hunting";
}