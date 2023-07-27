using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip grabCoinAudio;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioSource.PlayClipAtPoint(grabCoinAudio, transform.position);
            Destroy(gameObject);
        }

    }
}
