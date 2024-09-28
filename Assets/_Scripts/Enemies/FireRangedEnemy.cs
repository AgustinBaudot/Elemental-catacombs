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
        _type = "Ranged";
        _element = "Fire";
        _range = 7.5f;
        gameObject.tag = "Enemy";
    }
}
