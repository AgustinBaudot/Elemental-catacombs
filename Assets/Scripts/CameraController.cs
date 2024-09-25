using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int _offset;

    public void MoveCamera(Vector2 direction)
    {
        transform.position = new Vector2(transform.position.x + direction.x * _offset, transform.position.y + direction.y * _offset);
    }
}