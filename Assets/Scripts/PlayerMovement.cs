using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;


    private Rigidbody2D _rb;
    private Vector2 _movement;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        _movement = new Vector2(inputX, inputY);
    }

    private void FixedUpdate()
    {
        if (_movement == Vector2.zero)
        {
            _rb.linearVelocity = Vector2.zero;
        }
        else
        {

            _rb.linearVelocity = _movement * movementSpeed;
        }
    }
}
