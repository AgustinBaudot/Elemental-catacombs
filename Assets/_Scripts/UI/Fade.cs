using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image _fade;
    public UnityEvent FadeFinished = new UnityEvent();

    public void FadeIn()
    {
        StartCoroutine(FadeImage(true));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeImage(false));
    }

    private IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            _fade.gameObject.SetActive(true);
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                _fade.color = new Color(0, 0, 0, i);
                yield return null;
            }
            FadeFinished.Invoke();
        }

        else
        {
            _fade.gameObject.SetActive(true);
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                _fade.color = new Color(0, 0, 0, i);
                yield return null;
            }
            FadeFinished.Invoke();
        }
    }
}
