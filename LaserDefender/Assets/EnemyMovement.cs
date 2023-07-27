using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private List<Vector3> _waypoints = new List<Vector3>();
    public float speed = 3f;
    public float turnSpeed = 3f;
    private Vector3 _flightDirection = Vector3.down;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            _waypoints.Add(child.position);
            Destroy(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (_waypoints.Count >= 1)
        {
            Vector3 target = _waypoints.First();
            Vector3 direction = (target - currentPosition);


            _flightDirection = Vector3.RotateTowards(_flightDirection, direction, turnSpeed * Time.deltaTime, 0.0f);
            Debug.DrawRay(currentPosition, _flightDirection, Color.red);


            
            if ((target - transform.position).magnitude < 0.1f)
                _waypoints.RemoveAt(0);
        }
        
        transform.position = currentPosition + speed * Time.deltaTime * _flightDirection;

    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {

    }
}
