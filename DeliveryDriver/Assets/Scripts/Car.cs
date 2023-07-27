using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : MonoBehaviour
{
    public float carSpeed = 15f;
    public float turnSpeed = 180f;
    public bool gotPackage = false;
    private float? _boostTimer = null;
    public float boostMultiplier = 1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleTimer()
    {
        switch (_boostTimer)
        {
            case null:
                return;
            case >= 0:
                _boostTimer -= Time.deltaTime;
                break;
            default:
                _boostTimer = null;
                boostMultiplier = 1f;
                break;
        }        
    }
    
    // Update is called once per frame
    private void Update()
    {
        HandleTimer();
            
        // GetAxis es analógico, de ahi que decrezca de manera natural.
        var turnAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        var moveAmount = Input.GetAxis("Vertical") * carSpeed * boostMultiplier * Time.deltaTime;

        transform.Rotate(0,0,-turnAmount*Input.GetAxis("Vertical"));
        transform.Translate(0,moveAmount,0);
    }

    public void PackagePickUp()
    {
        gotPackage = true;
    }

    public void SpeedUp(float boostTimer, float boostMultiplier)
    {
        _boostTimer = boostTimer;
        this.boostMultiplier = boostMultiplier;
    }
    
    
    /* Funciona, pero es digital.
 
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(new Vector3(0, carSpeed * Time.deltaTime, 0));
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(new Vector3(0, -carSpeed * Time.deltaTime, 0));
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(new Vector3(0, 0, turnSpeed));
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(new Vector3(0, 0, -turnSpeed));
    }
*/


/* Experimentos (fallidos con el con RigidBody2D
 
void FixedUpdate()
{
    //var newDir = Vector3.Cross(transform.position, Vector3.forward).normalized; 
    //var q = Quaternion.FromToRotation(myRB.velocity, newDir);
    myRB.velocity = transform.up * myRB.velocity.magnitude;

    if (Input.GetKey(KeyCode.UpArrow))
        myRB.AddForce(transform.up * carSpeed * Time.fixedDeltaTime);
    if (Input.GetKey(KeyCode.DownArrow))
        myRB.AddForce(transform.up * -carSpeed * Time.fixedDeltaTime);
}

private void Update()
{
    if (Input.GetKey(KeyCode.LeftArrow))
        transform.Rotate(new Vector3(0,0,turnSpeed) * Time.deltaTime);
    if(Input.GetKey(KeyCode.RightArrow))
        transform.Rotate(new Vector3(0,0,-turnSpeed) * Time.deltaTime);
}
*/
}
