using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangedEnemy : Enemy
{
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private Transform _ballSpawnPoint;

    [SerializeField] private float _attackCoolDown = 1f;
    [SerializeField] private float _currentTime;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Start();

        _currentTime += Time.deltaTime;

        if(_currentTime >= _attackCoolDown)
        {
            RangedAttack();
            _currentTime = 0;
        }
    }

    public override void Init()
    {
        _hp = 50;
        _speed = 1.75f;
        _atkRange = 5f;
        _persueRange = 8;
        _attackCD = 2;
        _type = "Ranged";
        _element = "Fire";
        gameObject.tag = "Enemy";
    }

    private void RangedAttack()
    {
        Instantiate(_fireBall, _ballSpawnPoint.position, transform.rotation);
    }
}
