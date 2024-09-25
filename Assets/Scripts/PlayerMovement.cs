using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _cameraScript;
    [SerializeField] float speed;

    private Rigidbody2D rb;
    private Vector2 inputMovement;

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
}
