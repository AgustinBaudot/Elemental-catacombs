using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    [SerializeField] private float _ballSpeed = 5;
    [SerializeField] private float _currentTime = 0;
    [SerializeField] private float _lifeTime = 5;
    private Transform playerTransform;

    private PlayerHealth _playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _lifeTime)
        {
            Destroy(gameObject);
        }

        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            transform.position += (Vector3)direction * _ballSpeed * Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            _playerHealth.ReceiveDamage();
            Destroy(gameObject);
        }  
    }
}
