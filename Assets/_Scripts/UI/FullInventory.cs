using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullInventory : MonoBehaviour
{
    [SerializeField] private CanvasGroup _inventory;
    [SerializeField] private GameObject[] _potions;

    private void Start()
    {
        _inventory.alpha = 0;
        UpdatePotions(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        _inventory.alpha = (_inventory.alpha == 0) ? 1 : 0;
    }

    public void UpdatePotions(int currentPotions)
    {
        for (int i = 0; i < _potions.Length; i++)
        {
            if (i < currentPotions)
            {
                _potions[i].SetActive(true);
            }
            else
            {
                _potions[i].SetActive(false);
            }
        }
    }
}
