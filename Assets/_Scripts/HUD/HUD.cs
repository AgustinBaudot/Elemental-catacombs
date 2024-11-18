using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject[] _potions;
    [SerializeField] private Sprite[] _health;
    [SerializeField] private Sprite[] _healthBars;
    private GameObject _healthBar, _player;

    private void Start()
    {
        _healthBar = GameObject.Find("Health bar");
        _player = GameObject.Find("Player");
        UpdateHearts(5);
        UpdatePotions(0);
    }

    public void UpdateHearts(int currentHealth)
    {
        _healthBar.GetComponent<Image>().sprite = _health[currentHealth - 1];
    }

    public void UpdatePotions(int currentPotions)
    {
        if (currentPotions >= 2)
        {
            foreach (GameObject potion in _potions)
            {
                potion.SetActive(true);
            }
        }
        else if (currentPotions == 1)
        {
            _potions[0].SetActive(true);
            _potions[1].SetActive(false);
        }
        else if (currentPotions == 0)
        {
            foreach (GameObject potion in _potions)
            {
                potion.SetActive(false);
            }
        }
    }

    public void UpdateHealthBar()
    {
        var canAttack = _player.GetComponent<PlayerAttack>().GetAttackStatus();
        var canDash = _player.GetComponent<PlayerMovement>().GetDashStatus();
        var healthBar = GameObject.Find("Player values").GetComponent<Image>();
        if (!canDash && canAttack)
        {
            healthBar.sprite = _healthBars[0];
        }
        else if (canDash && !canAttack)
        {
            healthBar.sprite = _healthBars[1];
        }
        else if (canDash && canAttack)
        {
            healthBar.sprite = _healthBars[2];
        }
        else if (!canDash && !canAttack)
        {
            healthBar.sprite = _healthBars[3];
        }
    }
}
