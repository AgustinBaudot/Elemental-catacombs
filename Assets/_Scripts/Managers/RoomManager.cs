using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //array of transforms of rooms, check which of them is closer camera transform

    public static RoomManager _instance;
    [SerializeField] private Transform[] _rooms;
    [SerializeField] private bool[] _roomsClosed;
    [SerializeField] private GameObject _player;
    private Transform _closest;
    private bool _closed = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        _closest = GameObject.Find("Central grid").transform;
    }

    private void Update()
    {
        if (_closed)
        {
            if (_rooms[System.Array.IndexOf(_rooms, _closest)].childCount == 0)
            {
                _closed = false;
            }
        }
    }

    public void CurrentRoom()
    {
        foreach (Transform roomTransform in _rooms)
        {
            if (Vector2.Distance(roomTransform.position, _player.transform.position) < Vector2.Distance(_closest.position, _player.transform.position))
            {
                _closest = roomTransform;
            }
        }

        var roomIndex = System.Array.IndexOf(_rooms, _closest);
        if (!_roomsClosed[roomIndex]) //If room didn't close yet, we close it.
        {
            _roomsClosed[roomIndex] = true;
            _closed = true;
        }
    }

    public bool IsClosed() { return _closed; }
}
