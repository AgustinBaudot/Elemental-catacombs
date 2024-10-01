using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireMeleeEnemy : Enemy
{
    private PlayerHealth _playerHealth;

    new void Start()
    {
        base.Start();
    }

    public override void Init()
    {
        _hp = 100;
        _speed = 2f;
        _atkRange = 1.5f;
        _persueRange = 6;
        _attackCD = 0.5f;
        _type = "Melee";
        _element = "Fire";
        gameObject.tag = "Enemy";
        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override IEnumerator AttackCD(float attackCD)
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCD);
        _enemyState = EnemyState.Persuing;
        _canAttack = true;
        if (Physics2D.OverlapCircleAll(transform.position, 1.5f, 9) != null)
        {
            _playerHealth.ReceiveDamage();
            Debug.Log("player hit");
        }
        Debug.Log("Attacked");
    }

    //public void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere(transform.position, 1.5f);
    //}
}
