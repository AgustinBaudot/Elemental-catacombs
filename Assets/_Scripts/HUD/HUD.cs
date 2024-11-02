using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject[] _hearts;

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
}
