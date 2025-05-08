using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DistanceEnemy : Enemy, IProduct
{
    [Header("Configuración de Stats")]
    [SerializeField] private DistanceEnemyStats _stats;

    [Header("Waypoints")]
    [SerializeField] private Transform[] _waypoints;

    private float _energy;
    private Vector3 _velocity;
    private float _lastShootTime = -Mathf.Infinity;
    private FSM<string> _fsm;
    private float _currentMaxVelocity;
    private float _currentShootCooldown;

    public Vector3 Velocity => _velocity;

    public void Initialize()
    {
        //Debug.Log("Enemy Initialized");
    }

    private void Awake()
    {
        // Aplicar multiplicadores de dificultad
        float difficultyMultiplier = RemoteConfig.Instance != null ? RemoteConfig.Instance.GameDifficulty : 1f;
        _currentMaxVelocity = _stats.maxVelocity * difficultyMultiplier;
        _currentShootCooldown = _stats.shootCooldown / difficultyMultiplier; // Menor cooldown = más rápido dispara

        // Usar la energía desde Remote Config si está disponible
        float baseEnergy = RemoteConfig.Instance != null ? RemoteConfig.Instance.EnemyEnergy : _stats.maxEnergy;
        _energy = baseEnergy;

        _fsm = new FSM<string>();

        _fsm.AddState(EnemyDistanceStatesNames.Idle, new IdleState(
             _fsm,
             () => _energy,
             (value) => _energy = value,
             baseEnergy,
             _stats.speedReg,
             (v) => _velocity = v
         ));
        _fsm.AddState(EnemyDistanceStatesNames.Movement, new MovementState(
            _fsm,
            transform,
            _stats.radiusPersuit,
            _waypoints,
            (targetPos) => Seek(targetPos),
            (force) => AddForce(force),
            () => _energy,
            (value) => _energy = value
        ));
        _fsm.AddState(EnemyDistanceStatesNames.Hunting, new HuntingState(
            _fsm,
            transform,
            _stats.radiusPersuit,
            _stats.radiusAttack,
            (target) => Pursuit(target),
            (force) => AddForce(force),
            (target) => ShootAtTarget(target),
            () => _energy,
            (value) => _energy = value,
            (v) => _velocity = v
        ));

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
        _velocity = Vector3.ClampMagnitude(_velocity, _currentMaxVelocity);

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
        desired *= _currentMaxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _stats.maxForce);

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
        if (Time.time - _lastShootTime < _currentShootCooldown)
            return;

        _lastShootTime = Time.time;

        if (target == null) return;

        BulletEnemy bullet = BulletEnemyFactory.Instance.GetFromPool();
        if (bullet == null) return;

        Vector3 direction = (target.transform.position - transform.position).normalized;

        bullet.transform.position = transform.position;
        bullet.SetDirection(direction);
        bullet.SetOwner(this);
    }
}

public static class EnemyDistanceStatesNames
{
    public static string Idle = "Idle";
    public static string Movement = "Movement";
    public static string Hunting = "Hunting";
}