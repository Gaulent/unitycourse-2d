using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject laser;
    public float fireRate=5f;
    private float _waitToShoot = 0f;
    private GameObject _playerGo;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerGo = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _waitToShoot += Time.deltaTime;
        if (_waitToShoot >= 1f / fireRate)
        {
            Shoot();
            _waitToShoot = 0f;
        }        
    }

    void Shoot()
    {
        GameObject laserGo = Instantiate(laser, transform.position, Quaternion.identity);

        if(_playerGo!=null)
            laserGo.GetComponent<EnemyLaser>().direction = (_playerGo.transform.position - transform.position).normalized;
    }
    
}
