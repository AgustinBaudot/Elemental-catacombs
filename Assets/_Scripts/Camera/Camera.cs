using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private int _offset = 15;
    [SerializeField] private Transform _followTransform;
    [SerializeField] private Transform _player;

    public void MoveCamera(Vector2 direction)
    {
        _followTransform.position = new Vector2(_followTransform.position.x + direction.x * _offset, _followTransform.position.y + direction.y * _offset);
        _player.position = _followTransform.position;
    }
}