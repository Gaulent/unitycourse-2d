using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _boxCollider2D;
    private float? _timeOff = null;
    public float timeToReturn = 5f;
    public float boostTimer = 5f;
    public float boostMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<CircleCollider2D>();
    }

    void HandleTimer()
    {
        switch (_timeOff)
        {
            case null:
                return;
            case >= 0:
                _timeOff -= Time.deltaTime;
                break;
            default:
                EnablePickup();
                break;
        }        
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleTimer();
    }

    private void EnablePickup()
    {
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
        _timeOff = null;
    }
    
    private void DisablePickup()
    {
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
        _timeOff = timeToReturn;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Car car = col.GetComponent<Car>();
        car.SpeedUp(boostTimer, boostMultiplier);
        DisablePickup();
    }
}