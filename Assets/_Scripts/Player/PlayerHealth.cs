using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int _health = 5;
    private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GetComponent<PlayerMovement>()._potions > 0) UsePotion();
        }

        Dies();
    }

    public void ReceiveDamage(int damageReceived = 1)
    {
        //if enemy successfuly attacks player:
        _health-= damageReceived;
    }

    public void UsePotion()
    {
        GetComponent<PlayerMovement>()._potions--;
    }

    private void Dies()
    {
        if (_health <= 0) 
        { 
            Destroy(gameObject);
        }
    }
}