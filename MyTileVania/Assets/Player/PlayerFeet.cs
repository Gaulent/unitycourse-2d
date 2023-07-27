using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private LayerMask enemy;
    private PlayerController _myPC;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = LayerMask.GetMask("Enemy");
        _myPC = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            _myPC.EnemyKilled();
            col.gameObject.GetComponent<Enemy>().GetKilled();
        }
    }
}
