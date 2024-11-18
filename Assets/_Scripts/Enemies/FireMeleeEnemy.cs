using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireMeleeEnemy : Enemy
{
    private PlayerHealth _playerHealth;
    [SerializeField] private Animator _animator;

    new void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    public override void Init()
    {
        _hp = 30;
        _speed = 2;
        _atkRange = 1.5f;
        _persueRange = 6;
        _attackCD = 1.5f;
        _type = "Melee";
        _element = "Fire";
        gameObject.tag = "Enemy";
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override IEnumerator AttackCD(float attackCD)
    {
        _animator.SetBool("enemyAttacking", true);
        _canAttack = false;

        yield return new WaitForSeconds(attackCD);

        var collisions = Physics2D.OverlapCircleAll(transform.position, _atkRange);

        if (collisions.Length > 0)
        {
            foreach (Collider2D collision in collisions)
            {
                if (collision.gameObject.name == "Player")
                {
                    _playerHealth.ReceiveDamage();
                }
            }
        }

        _animator.SetBool("enemyAttacking", false);
        _enemyState = EnemyState.Persuing;
        _canAttack = true;
    }
}
