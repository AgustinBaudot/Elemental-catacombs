using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    [SerializeField] private float _ballSpeed = 5;
    [SerializeField] private float _currentTime = 0;
    [SerializeField] private float _lifeTime = 5;
    [SerializeField] private float _persueTime = 1;
    private Transform _playerTransform;
    private Vector2 _direction;

    private PlayerHealth _playerHealth;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime <= _persueTime)
        {
            _direction = (_playerTransform.position - transform.position).normalized;
        }

        if (_currentTime >= _lifeTime)
        {
            Destroy(gameObject);
        }

        if (_playerTransform != null)
        {
            transform.position += (Vector3)_direction * _ballSpeed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            _playerHealth.ReceiveDamage(2);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
