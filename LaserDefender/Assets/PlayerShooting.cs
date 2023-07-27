using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public InputAction fireInput;
    public GameObject laser;
    public float fireRate=5f;
    private float _waitToShoot = 0f;
    private bool _firing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        _waitToShoot += Time.deltaTime;
        
        if (_firing && _waitToShoot >= 1f / fireRate)
        {
            Shoot();
            _waitToShoot = 0f;
        }
    }

    void OnFire(InputValue fire)
    {
        _firing = fire.isPressed;
    }
    
    
    void Shoot()
    {
        Instantiate(laser, transform.position, Quaternion.identity);
    }
}
