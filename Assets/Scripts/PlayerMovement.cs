using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _cameraScript;
    [SerializeField] float _speed;
    [SerializeField] float _dashSpeed;
    [SerializeField] private float _dashCD; //Colldown between dashes.
    [SerializeField] private float _dashTime; //Dash duration.

    private Rigidbody2D _rb;
    private Vector2 _inputMovement;

    private bool _canDash = true;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; //Input movement.

        if (Input.GetKeyDown(KeyCode.Space) && _canDash) //If player presses space, he dashes.
        {
            Dash();
        }

        if (_inputMovement.magnitude != 0)
        {
            transform.rotation = (_inputMovement.x > 0) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        ManageMovement();
    }

    private void Dash() //Player dashes.
    {
        _canDash = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        _speed *= _dashSpeed;
        StartCoroutine(DashTime(_dashTime));
    }

    private IEnumerator DashTime(float dashDuration) //Dash duration time.
    {
        yield return new WaitForSeconds(dashDuration);
        _speed /= _dashSpeed;
        GetComponent<CapsuleCollider2D>().enabled = true;
        StartCoroutine(DashCD(_dashCD));
    }

    private IEnumerator DashCD(float dashCD) //Dash cooldown time.
    {
        yield return new WaitForSeconds(dashCD);
        _canDash = true;
    }

    private void ManageMovement()
    {
        _rb.MovePosition(transform.position + new Vector3(_inputMovement.x, _inputMovement.y, 0) * Time.deltaTime * _speed);
    } //Player movement.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (collision.gameObject.name == "Left Exit")
            {
                _cameraScript.MoveCamera(Vector2.left);
            }
            else if (collision.gameObject.name == "Down Exit")
            {
                _cameraScript.MoveCamera(Vector2.down);
            }
            else if (collision.gameObject.name == "Up Exit")
            {
                _cameraScript.MoveCamera(Vector2.up);
            }
            else if (collision.gameObject.name == "Right Exit")
            {
                _cameraScript.MoveCamera(Vector2.right);
            }
        }
    }
}
