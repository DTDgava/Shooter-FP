using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controller;
    public Image Health;
    float PlayerHeatlh = 100;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;
    bool isCrouching;

    Vector3 velocity;
    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        Crouch();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 2;
            controller.height = .5f;
            isCrouching = true;
        }
        else
        {
            speed = 4;
            controller.height = 2;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isCrouching = true;
                return;
            }
            isCrouching = false;
        }
    }
}
