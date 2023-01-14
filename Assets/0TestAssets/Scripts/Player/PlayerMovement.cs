using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    void Movement()
    {
        //if (Input.GetKeyDown(KeyCode.W)) transform.position += Vector3.MoveTowards(transform.position, Vector3.forward, Time.deltaTime * movementSpeed);
        //if (Input.GetKeyDown(KeyCode.S)) transform.position += Vector3.MoveTowards(transform.position, Vector3.back, Time.deltaTime * movementSpeed);
        //if (Input.GetKeyDown(KeyCode.A)) transform.position += Vector3.MoveTowards(transform.position, Vector3.left, Time.deltaTime * movementSpeed);
        //if (Input.GetKeyDown(KeyCode.D)) transform.position += Vector3.MoveTowards(transform.position, Vector3.right, Time.deltaTime * movementSpeed);

        var vel = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movementSpeed;// * Time.deltaTime;

        vel.y = _rb.velocity.y;
        _rb.velocity = vel;


        if(Input.GetKeyDown(KeyCode.Space)) _rb.AddForce(Vector3.up * _jumpSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
