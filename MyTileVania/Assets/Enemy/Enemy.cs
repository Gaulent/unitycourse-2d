using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _myRB;
    public float speed = 2f;
    private BoxCollider2D _myCollider;
    private CapsuleCollider2D _myHurtCollider;
    public bool facingRight = true;
    private SpriteRenderer _mySR;
    private LayerMask groundLayer;
    private LayerMask turnOnLayer;
    [SerializeField] private Material fadeMaterial;
    private Animator _animator;

    
    // Start is called before the first frame update
    void Start()
    {
        _mySR = GetComponent<SpriteRenderer>();
        _myRB = GetComponent<Rigidbody2D>();
        _myCollider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        turnOnLayer = LayerMask.GetMask("Ground", "Enemy");
        _myHurtCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TurnMovement()
    {
        facingRight = !facingRight;
        _mySR.flipX = !facingRight;
    }

   
    public void GetKilled()
    {
        _mySR.material = fadeMaterial;
        _mySR.material.SetColor("_Color",Color.red);
        _myCollider.enabled = false;
        _myHurtCollider.enabled = false;
        _animator.SetTrigger("GetHit");
    }

    public void GetDestroyed()
    {
        Destroy(gameObject);
    }
    
    private void FixedUpdate()
    {
        if (facingRight)
        {
            _myRB.velocity = new Vector2(speed, _myRB.velocity.y);
        }
        else
        {
            _myRB.velocity = new Vector2(-speed, _myRB.velocity.y);
        }

        HandleFloorEnd();
        HandleFrontCollisions();
    }

    private void HandleFloorEnd()
    {
        var b = _myCollider.bounds;
        Vector3 rayStart;
        if (facingRight)
        {
            rayStart = b.center + new Vector3(b.extents.x / 2, -b.extents.y+0.1f, 0);
        }
        else
        {
            rayStart = b.center + new Vector3(-b.extents.x / 2, -b.extents.y+0.1f, 0);
        }


        if (!Physics2D.Raycast(rayStart, Vector2.down, 0.2f, groundLayer))
            TurnMovement();

        Debug.DrawRay(rayStart,Vector2.down*0.2f);
    }
    
    private void HandleFrontCollisions()
    {
        if (facingRight)
        {
            Vector3 rayStart = _myCollider.bounds.center + new Vector3(_myCollider.bounds.extents.x + 0.1f,0,0);
            Debug.DrawRay(rayStart, Vector2.right*0.1f);
            if (Physics2D.Raycast(rayStart, Vector2.right, 0.1f, turnOnLayer))
                TurnMovement();
        }
        else
        {
            Vector3 rayStart = _myCollider.bounds.center - new Vector3(_myCollider.bounds.extents.x + 0.1f,0,0);
            Debug.DrawRay(rayStart, Vector2.left*0.1f);
            if (Physics2D.Raycast(rayStart, Vector2.left, 0.1f, turnOnLayer))
                TurnMovement();
        }
    }
}
