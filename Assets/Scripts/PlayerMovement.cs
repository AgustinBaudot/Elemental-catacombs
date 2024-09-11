using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        //if (inputMovement.x != 0 && inputMovement.y != 0)
        //{
        //    //Player is moving diagonally
        //    rb.MovePosition(transform.position + new Vector3(NormalizeDiagonal(inputMovement).x, NormalizeDiagonal(inputMovement).y, 0) * Time.deltaTime * speed);
        //    Debug.Log("Diagonally");
        //}
        //else
        //{
        //    rb.MovePosition(transform.position + new Vector3(inputMovement.x, inputMovement.y, 0) * Time.deltaTime * speed);
        //    Debug.Log("Not diagonally");
        //}
        rb.MovePosition(transform.position + new Vector3(inputMovement.x, inputMovement.y, 0) * Time.deltaTime * speed);
    }

    private Vector2 NormalizeDiagonal(Vector2 diagonalVector)
    {
        inputMovement.SqrMagnitude();
        return inputMovement;
    }
}
