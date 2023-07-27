using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowBoarder : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    public float turnSpeed;
    private bool onTheGround;
    private ParticleSystem ps;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onTheGround)
        {
            //ps.enableEmission = true; // Deprecated?
            var emission = ps.emission;
            emission.enabled = true;
        }
        else
        {
            var emission = ps.emission;
            emission.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        myRigidbody2D.AddTorque(Input.GetAxis("Horizontal") * Time.fixedDeltaTime * turnSpeed * -1);
        

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        onTheGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onTheGround = false;
    }
 
}
