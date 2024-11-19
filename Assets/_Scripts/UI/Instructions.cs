using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Instructions : MonoBehaviour
{

    [SerializeField] private Image _instructionsFrame;
    private Coroutine _co;
    bool _potionsMessage = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            Level1();
        }
        else
        {
            _instructionsFrame.gameObject.SetActive(false);
        }
    }

    public void Level1()
    {
        if (RoomManager._instance.GetCurrentRoom().name == "Left grid")
        {
            _instructionsFrame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Move with WASD or arrows.\nUse space to dash.";
        }
        else if (RoomManager._instance.GetCurrentRoom().name == "Central grid")
        {
            if (_co != null) StopCoroutine(_co);
            _instructionsFrame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Oh no, an enemy!\nUse left click to defeat him!";
            _instructionsFrame.gameObject.SetActive(true);
        }
        else if (RoomManager._instance.GetCurrentRoom().name == "Right grid")
        {
            if (_co != null) StopCoroutine(_co);
            _instructionsFrame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "This one shoots!\nUse the dash to avoid being hit!";
            _instructionsFrame.gameObject.SetActive(true);
        }
        _co = StartCoroutine(Timer(3));
    }

    public void Level2()
    {
        var player = GameObject.Find("Player");
        if (RoomManager._instance.GetCurrentRoom().name == "Central grid" && player.GetComponent<PlayerMovement>()._potions == 1)
        {
            if (_co != null) StopCoroutine(_co);
            _instructionsFrame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You found a potion!\nPressing Q will heal your wounds.";
            _instructionsFrame.gameObject.SetActive(true);
        }
        if (player.GetComponent<PlayerMovement>()._potions >= 3 && !_potionsMessage)
        {
            if (_co != null) StopCoroutine(_co);
            _instructionsFrame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You have lots of potions, well done!\nPress I to open and close your inventory.";
            _instructionsFrame.gameObject.SetActive(true);
            _potionsMessage = true;
        }
        _co = StartCoroutine(Timer(2.5f));
    }

    private IEnumerator Timer(float timeduration)
    {
        yield return new WaitForSeconds(timeduration);
        _instructionsFrame.gameObject.SetActive(false);
    }
}
