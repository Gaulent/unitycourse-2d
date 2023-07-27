using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private float? _timeOff = null;
    public float timeToReturn = 5f;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _gameManager = FindObjectOfType<GameManager>();
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

        if (car.gotPackage)
        {
            car.gotPackage = false;
            DisablePickup();
            _gameManager.AddPoints();
        }

    }
}