using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _hp;
    protected float _speed, _range;
    protected string _type, _element;
    [SerializeField] protected GameObject _player;
    [SerializeField] protected EnemyState _enemyState;
    protected enum EnemyState { Persuing, Attacking };

    public virtual void Init()
    {
        tag = "Enemy";
    }

    public virtual void Start()
    {
        _enemyState = EnemyState.Persuing;
        Init();
    }

    protected void Update()
    {
        if (_enemyState == EnemyState.Persuing)
        {
            if (Vector2.Distance(_player.transform.position, transform.position) < _range)
            {
                _enemyState = EnemyState.Attacking;
                return;
            }

            var step = _speed * Time.deltaTime; //Calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step);
        }

        else if (_enemyState == EnemyState.Attacking)
        {
            Attack();
        }
    }

    public virtual void ReceiveDamage(int damageReceived)
    {
        _hp -= damageReceived;
        Debug.Log(damageReceived);
        if (_hp <= 0)
        {
            Dies();
        }
    }

    public virtual void Dies()
    {
        //Add anim.
        Destroy(gameObject);
    }

    public virtual void Attack()
    {
        _enemyState = EnemyState.Persuing;
        //Attack;
    }
}
