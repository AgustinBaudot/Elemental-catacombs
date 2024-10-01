using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRangedEnemy : Enemy
{
    new void Start()
    {
        base.Start();
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
}
