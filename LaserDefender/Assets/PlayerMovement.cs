using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D _myRB;
    private Vector2 _moveAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    void OnMove(InputValue axis)
    {
        _moveAmount = axis.Get<Vector2>().normalized * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _myRB.MovePosition((Vector2)transform.position + _moveAmount * Time.fixedDeltaTime);
    }
}