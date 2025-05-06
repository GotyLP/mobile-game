using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float dmg = 5f;

    private float _currentLifeTime;
    private Vector3 _direction;

    public void SetDirection(Vector3 dir)
    {
        _direction = dir.normalized;
    }

    private void OnEnable()
    {
        _currentLifeTime = _lifeTime;
    }

    private void Update()
    {
        transform.position += _direction * (speed * Time.deltaTime);

        _currentLifeTime -= Time.deltaTime;

        if (_currentLifeTime <= 0)
        {
            BulletEnemyFactory.Instance.ReturnToPool(this);
        }
    }

    public static void TurnOn(BulletEnemy bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    public static void TurnOff(BulletEnemy bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisiono");

        IEntity entity = other.GetComponent<IEntity>();
        if (entity != null)
        {
            Debug.Log("Entro");
            entity.GetDamage(dmg);
            TurnOff(this);
        }
    }
}
