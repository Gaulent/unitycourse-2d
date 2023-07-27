using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private LayerMask _damageLm;
    
    // Start is called before the first frame update
    void Start()
    {
        _damageLm = LayerMask.GetMask("Enemy", "EnemyLaser");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(_damageLm.Contains(col.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }
}
