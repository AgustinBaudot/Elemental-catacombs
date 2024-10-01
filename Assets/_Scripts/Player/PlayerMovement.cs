using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _cameraScript;
    [SerializeField] private float _speed, _dashSpeed; //Normal moving speed and dashing speed;
    [SerializeField] private float _dashCD, _dashTime; //Colldown between dashes & dash duration;
    public int _potions;

    private Rigidbody2D _rb;
    private Vector2 _inputMovement;
    private Animator _anim;

    private bool _canDash = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
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

        _anim.speed = (HitStopManager._instance.IsStopped()) ? 0 : 1; //Set animation time to 0 if time is stopped.
        ManageAnimations();
    }

    void FixedUpdate()
    {
        if (!HitStopManager._instance.IsStopped()) ManageMovement();
    }

    private void Dash() //Player dashes.
    {
        _canDash = false;
        Physics2D.IgnoreLayerCollision(0, 7, true);
        Physics2D.IgnoreLayerCollision(0, 6, true);
        _speed *= _dashSpeed;
        StartCoroutine(DashTime(_dashTime));
    }

    private IEnumerator DashTime(float dashDuration) //Dash duration time.
    {
        yield return new WaitForSeconds(dashDuration);
        _speed /= _dashSpeed;
        Physics2D.IgnoreLayerCollision(0, 7, false);
        Physics2D.IgnoreLayerCollision(0, 6, false);
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
                Debug.Log("collided");
                _cameraScript.MoveCamera(Vector2.left);
            }
            else if (collision.gameObject.name == "Bottom Exit")
            {
                _cameraScript.MoveCamera(Vector2.down);
            }
            else if (collision.gameObject.name == "Top Exit")
            {
                _cameraScript.MoveCamera(Vector2.up);
            }
            else if (collision.gameObject.name == "Right Exit")
            {
                _cameraScript.MoveCamera(Vector2.right);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            _potions ++;
            Destroy(collision.gameObject);
        }
    }

    private void ManageAnimations()
    {
        _anim.SetFloat("Horizontal", _inputMovement.x);
        _anim.SetFloat("Vertical", _inputMovement.y);

        _anim.SetFloat("Speed", _inputMovement.sqrMagnitude);
    }
}
