using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    
    private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private GameObject _gameOverScreen, _winScreen;
    [SerializeField] private Fade _fadeScript;

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
            if (!_winScreen.activeSelf) Win();
        }
    }

    public void Win()
    {
        _winScreen.SetActive(true);
    }

    public void Lose()
    {
       _gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        _fadeScript.FadeOut();
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        _fadeScript.FadeOut();
        StartCoroutine(ChangeScene(0));
    }

    private IEnumerator ChangeScene(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }

    public void EnemyDeath(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }
}
