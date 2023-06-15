using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarScript : MonoBehaviour
{
    [SerializeField] private float _speed = 0;
    [SerializeField] private float _turning_speed = 50;
    [SerializeField] private float _max_speed = 5f;
    [SerializeField] private float _time_to_max = 2.5f;
    [SerializeField] public float acceleration_per_second = 0.5f;
    public float decceleration_per_second = 1.5f;
    public Rigidbody2D myRigidBody;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
            movementDirection.Normalize();

            transform.Translate(movementDirection * _speed * inputMagnitude * Time.deltaTime, Space.World);

            if (movementDirection != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _turning_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _speed += acceleration_per_second * Time.deltaTime;
                _speed = Mathf.Min(_speed, _max_speed);
            }
            if (_speed >= 0)
            {
                //transform.Translate(Vector3.up * _speed * Time.deltaTime);
                _speed -= decceleration_per_second * Time.deltaTime;
            }
        }
        
    
    }
}
