using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

    public float maxSpeed = 100;
    public float acceleration = 1;
    public float handling = 1;

    private float speed = 0;
    private bool canMove = true;
    private Rigidbody rigidbody;
    private Vector3 moveDirection;
    private CharacterController controller;

    private float gravity = 9.8f;

    private float distToGround;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (!canMove) return;

        speed += Input.GetAxis("Vertical") * acceleration;

        if (speed > maxSpeed) speed = maxSpeed;

        if (speed < 0) speed = 0;

        speed -= (speed / 10);

        // Move senteces
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * handling);

        moveDirection = new Vector3(speed, 0, 0);
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
