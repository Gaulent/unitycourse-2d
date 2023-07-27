using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStatus {Grounded, Jumping, Climbing};

    
    private Rigidbody2D _myRB;
    public float speed = 350f;
    public float jumpForce = 100f;
    public float gravity = 4f;
    public PlayerStatus myStatus;
    private float moveAmount;
    private bool _goingToJump = false;
    private SpriteRenderer _mySR;
    private Animator _animator;
    private BoxCollider2D _feetTrigger;
    public LayerMask layer;
    public float coyoteTime = 0.01f;
    private float currentCoyoteTime = 0.01f;
    private bool _cutJump = false;
    private CapsuleCollider2D _myCol;
    private float climbAmount;
    public bool outOfControl = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        _myRB.gravityScale = gravity;
        myStatus = PlayerStatus.Grounded;
        _mySR = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _feetTrigger = GetComponentInChildren<BoxCollider2D>();
        _myCol = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Guardar movimiento

        if (!outOfControl)
        {
            moveAmount = Input.GetAxisRaw("Horizontal") * speed;

            switch (myStatus)
            {
                case PlayerStatus.Grounded:
                {
                    currentCoyoteTime = coyoteTime;

                    if (Input.GetButtonDown("Jump"))
                    {
                        _goingToJump = true;
                    }

                    _myRB.gravityScale = gravity;

                    break;
                }
                case PlayerStatus.Jumping:
                {
                    currentCoyoteTime -= Time.deltaTime;

                    if (currentCoyoteTime >= 0f && Input.GetButtonDown("Jump"))
                        _goingToJump = true;

                    if (currentCoyoteTime >= 0f && Input.GetButtonUp("Jump"))
                        _cutJump = true;

                    if (_myRB.velocity.y < 0)
                        _myRB.gravityScale = gravity * 1.5f;
                    else
                    {
                        _myRB.gravityScale = gravity;
                    }

                    break;
                }
                case PlayerStatus.Climbing:
                {
                    climbAmount = Input.GetAxisRaw("Vertical") * speed;
                    _myRB.gravityScale = 0f;

                    if (Input.GetButtonDown("Jump"))
                    {
                        _goingToJump = true;
                    }

                    break;
                }
            }
        }

        HandleLadders();
        HandleStatus();
        FlipSprite();
    }


    // TODO: Gestionar el sentido del salto y que no te de al cargarte un enemigo
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        
        
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            outOfControl = true;
            _animator.SetTrigger("hurt");

            var contact = col.contacts.First();
            if(contact.point.x > transform.position.x) 
                _myRB.velocity = (new Vector2(-2f, 5f));
            else
                _myRB.velocity = (new Vector2(2f, 5f));
        }
        
        if (col.gameObject.layer == LayerMask.NameToLayer("Shroom"))
        {
            _myRB.velocity = (new Vector2(0f, 20f));
        }        
        
        
    }

    // Llamado desde el Animator
    public void RecoverControl()
    {
        outOfControl = false;
    }
    

    private void HandleLadders()
    {
        if (_myCol.IsTouchingLayers(LayerMask.GetMask("Ladder")) && Input.GetAxis("Vertical") > 0.1f && myStatus != PlayerStatus.Climbing)
        {
            myStatus = PlayerStatus.Climbing;
        }
        if(!_myCol.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myStatus = PlayerStatus.Grounded;
        }
    }
    
    public void EnemyKilled()
    {
        _myRB.velocity = new Vector2(_myRB.velocity.x, jumpForce*0.75f);
    }
    

    void FlipSprite()
    {
        if (moveAmount < 0)
            _mySR.flipX = true;
        if (moveAmount > 0)
            _mySR.flipX = false;        
    }

    void HandleStatus()
    {
        if (myStatus == PlayerStatus.Climbing)
        {
            _animator.SetBool("grounded", false);
            _animator.SetBool("climbing",true);

        }
        else
        {
            _animator.SetBool("grounded",IsGrounded());
            _animator.SetBool("climbing",false);
            
            if (IsGrounded())
            {
                myStatus = PlayerStatus.Grounded;
                _animator.SetBool("moving",myStatus==PlayerStatus.Grounded && Mathf.Abs(_myRB.velocity.x) > 0.0001f);
            }
            else
            {
                myStatus = PlayerStatus.Jumping;
                switch (_myRB.velocity.y)
                {
                    case < 0:
                        _animator.SetTrigger("goingDown");
                        break;
                    case > 0:
                        _animator.SetTrigger("goingUp");
                        break;
                }
            }
        }
    }


    bool IsGrounded()
    {
        return _feetTrigger.IsTouchingLayers(layer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!outOfControl)
        {

            // Controlar movimiento
            _myRB.velocity = new Vector2(moveAmount * Time.fixedDeltaTime, _myRB.velocity.y);

            if (myStatus == PlayerStatus.Climbing)
                _myRB.velocity = new Vector2(moveAmount * Time.fixedDeltaTime, climbAmount * Time.fixedDeltaTime);

            if (_goingToJump)
            {
                _myRB.velocity = new Vector2(_myRB.velocity.x, jumpForce);
                myStatus = PlayerStatus.Jumping;
                _goingToJump = false;
            }

            if (_cutJump)
            {
                // No se siente del natural
                _myRB.velocity = new Vector2(_myRB.velocity.x, _myRB.velocity.y * 0.5f);
                _cutJump = false;
            }
        }
    }
}

