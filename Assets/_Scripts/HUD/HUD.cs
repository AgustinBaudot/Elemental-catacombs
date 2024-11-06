using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject[] _hearts, _potions;


    private void Start()
    {
        UpdateHearts(5);
        UpdatePotions(0);
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                _hearts[i].SetActive(true);
            }
            else
            {
                _hearts[i].SetActive(false);
            }
        }
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
}
