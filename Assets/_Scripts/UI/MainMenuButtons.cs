using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField] private Fade _fadeScript;
    [SerializeField] private Image _image;

    private void Start()
    {
        _fadeScript.FadeIn();
    }

    public void Play()
    {
        _fadeScript.FadeOut();
        StartCoroutine(ChangeScene(SceneManager.GetSceneByName("Playground").buildIndex));
    }

    public void Options()
    {
        _fadeScript.FadeOut();
        StartCoroutine(ChangeScene(SceneManager.GetSceneByName("Options").buildIndex));
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator ChangeScene(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
