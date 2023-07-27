using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Car _car;
    private Camera _camera;
    public float AdjustSpeed = 0.5f;
    public float BaseSize = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        _car = FindObjectOfType<Car>();
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() // Recordar que si la camara no es el ultimo en ejecutarse se ve raro
    { 
        transform.position = _car.gameObject.transform.position + Vector3.back;



        if ((int)_camera.orthographicSize > (int)(_car.boostMultiplier * BaseSize))
            _camera.orthographicSize -= AdjustSpeed * Time.deltaTime;
        else if ((int)_camera.orthographicSize < (int)(_car.boostMultiplier * BaseSize))
            _camera.orthographicSize += AdjustSpeed * Time.deltaTime;
    }
}
