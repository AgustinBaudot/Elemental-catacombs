using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject objet in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            _enemies.Add(objet);
        }
    }

    private void Update()
    {
        if (_enemies.Count == 0)
        {
            Debug.Log("WIN");
        }
    }

    public void Lose()
    {
        Debug.Log("LOSE");
    }
}
