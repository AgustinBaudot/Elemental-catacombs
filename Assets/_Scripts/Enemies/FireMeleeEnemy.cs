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
        _attackCD = 0.75f;
        _type = "Melee";
        _element = "Fire";
        gameObject.tag = "Enemy";
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            _playerHealth.ReceiveDamage();
        }
    }
}
