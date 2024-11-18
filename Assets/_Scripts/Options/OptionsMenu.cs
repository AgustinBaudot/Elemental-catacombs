using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Fade _fadeScript;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Volume") == 1)
        {
            GameObject.Find("Volume?").GetComponent<Toggle>().isOn = false;
        }

        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            GameObject.Find("Full screen?").GetComponent<Toggle>().isOn = false;
        }
    }

    public void SetMusic(bool hasVolume)
    {
        var vol = (hasVolume) ? 0 : 1; //Set it to 1 if false, 0 otherwise.
        PlayerPrefs.SetInt("Volume", vol);
    }

    public void ToggleFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        var full = (isFull) ? 0 : 1;
        PlayerPrefs.SetInt("Fullscreen", full); //Set it to 1 if fase, 0 otherwise.
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
}
