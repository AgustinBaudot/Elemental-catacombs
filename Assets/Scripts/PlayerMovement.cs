using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _cameraScript;
    [SerializeField] float speed;

    private Rigidbody2D rb;
    private Vector2 inputMovement;

    private bool _canDash = true;
    private bool _isDashing;
    private float _dashingTime = 0.2f;
    private float _dashingCooldown = 0.2f;
    [SerializeField] private float _dashingForce = 5f;




    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash) 
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        ManageMovement();
    }

    void ManageMovement()
    {
        rb.MovePosition(transform.position + new Vector3(inputMovement.x, inputMovement.y, 0) * Time.deltaTime * speed);
    }

    private Vector2 NormalizeDiagonal(Vector2 diagonalVector)
    {
        inputMovement.SqrMagnitude();
        return inputMovement;
    }

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

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * _dashingForce, 0);
        yield return new WaitForSeconds(_dashingTime);
        rb.gravityScale = originalGravity;
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}
