using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float boostTimer = 5f;
    public float boostMultiplier = 0.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        Car car = col.gameObject.GetComponent<Car>();
        car.SpeedUp(boostTimer, boostMultiplier);
    }

}
