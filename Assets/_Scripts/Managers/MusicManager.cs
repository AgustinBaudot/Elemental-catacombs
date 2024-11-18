using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private List<GameObject> _musicObjets = new List<GameObject>();

    private void Awake()
    {
        _musicObjets.Add(GameObject.Find("Music"));
        _musicObjets.Add(GameObject.Find("SFX"));
        if (PlayerPrefs.GetInt("Volume") == 1)
        {
            foreach (GameObject objet in _musicObjets) { objet.SetActive(false); }
        }
    }
}
