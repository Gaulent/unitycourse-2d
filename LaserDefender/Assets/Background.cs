using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private MeshRenderer _myMR;
    private float _offsetX = 0f;
    public float speed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _myMR = GetComponent<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _offsetX = (_offsetX + speed * Time.deltaTime) % 1;
        _myMR.material.mainTextureOffset = new Vector2(0, _offsetX);
    }
}
