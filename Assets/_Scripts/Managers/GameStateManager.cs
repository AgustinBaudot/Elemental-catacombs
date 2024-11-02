using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    public GameObject GameOverScreen;

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
       GameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
