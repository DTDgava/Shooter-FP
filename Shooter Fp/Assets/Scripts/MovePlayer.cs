using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    public CharacterController controller;
    public Image Health;
    public Text Medicamenttext;


    public float PlayerHeatlh = 100;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float medicaments;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    bool isGrounded;
    bool isCrouching;

    Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        useMedic();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Medicaments")
        {
            if (medicaments <= 3)
                medicaments++;
            Destroy(other.gameObject, 5f);
            Medicamenttext.text = "" + medicaments;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RadioActive")
        {
            PlayerHeatlh = PlayerHeatlh - 15;
            Health.fillAmount = PlayerHeatlh / 100;
        }
        if(PlayerHeatlh <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void useMedic()
    {
        if (Input.GetKey(KeyCode.Alpha1) && medicaments >= 1)
        {
            medicaments--;
            if (PlayerHeatlh > 50)
            {
                PlayerHeatlh += 50;
            }
            else if (PlayerHeatlh <= 50)
            {
                PlayerHeatlh = 100;
            }
            Medicamenttext.text = "" + medicaments;
            Health.fillAmount = PlayerHeatlh / 100;
        }
    }
    
}
