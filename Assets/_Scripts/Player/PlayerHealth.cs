using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GetComponent<PlayerMovement>()._potions > 0) UsePotion();
        }
    }

    public void ReceiveDamage()
    {
        //if enemy successfuly attacks player:
        _health--;
    }

    public void UsePotion()
    {
        GetComponent<PlayerMovement>()._potions--;
    }
}