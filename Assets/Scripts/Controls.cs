using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float maxSpeed;
    public float acceleration;
    public float handling;
    public float rollMax;

    private float speed = 0;
    private bool canMove = true;
    private Rigidbody rigidbody;
    private Vector3 moveDirection;
    private CharacterController controller;
    private float drag;
    private float gravity = 9.8f;
    private float distToGround;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        drag = maxSpeed / acceleration;
	}
	
	// Update is called once per frame
	void Update () {
        if (!canMove) return;

        speed += Input.GetAxis("Vertical") * acceleration;

        if (speed < 0) speed = 0;

        if (Input.GetAxis("Vertical") == 0) {
            speed -= (speed / 10);
        }
        else {
            speed -= (speed / drag);
        }

        if (speed > maxSpeed) speed = maxSpeed;

        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * handling);

        float roll = -Input.GetAxis("Horizontal") * rollMax * (speed/maxSpeed);  
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, roll);

        moveDirection = new Vector3(0, 0, speed);
        if (!IsGrounded()) {
            moveDirection.y -= gravity;
        }
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * Time.deltaTime);
    }

    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
