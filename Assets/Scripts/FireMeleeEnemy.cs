using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

public class FireMeleeEnemy : Enemy
{

    new void Start()
    {
        base.Start();
    }

    public override void Init()
    {
        _hp = 100;
        _speed = 2f;
        _type = "Melee";
        _element = "Fire";
        _range = 1.5f;
        gameObject.tag = "Enemy";
    }
}
