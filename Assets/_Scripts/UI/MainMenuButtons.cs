using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField] private Fade _fadeScript;
    [SerializeField] private Image _image;

    public void Play()
    {
        _fadeScript.FadeOut();
        StartCoroutine(ChangeScene(1));
    }

    public void Options()
    {
        _fadeScript.FadeOut();
        StartCoroutine(ChangeScene(2));
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
