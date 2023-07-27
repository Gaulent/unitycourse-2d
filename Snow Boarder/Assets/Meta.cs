using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{
    public GameObject effect;
    private AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Invoke(nameof(RestartLevel),0.5f); // Retrasar la ejecucion de un metodo
        Instantiate(effect,transform.position,transform.rotation);
        audio.Play();
        Debug.Log("Test");
    }
    
    
    private void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
