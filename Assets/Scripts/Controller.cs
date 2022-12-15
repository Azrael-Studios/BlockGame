using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Tooltip("Rigidbody component attached to the player")]
    public Rigidbody rb;
    public float health;
    public float maxHealth = 100;
    public GameObject deadMenu;
    public HealthBar healthBar;
    public Transform camera;

    private float movementX;
    private float movementY;
    private float gravity = -9.81f;
    private float speedX = 10;
    private float speedY = 0.0003f;
    private float speedZ = 5;
    private int jumpTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -100, 0);
        gameObject.tag = "Player";
        health = maxHealth;
    }
    void Update()
    {
        
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnJump()
    {
        jumpTime = 25;
    }

    void OnLook(InputValue rotationValue)
    {
        Vector2 rotationVector = rotationValue.Get<Vector2>();
        rotationVector.x = Mathf.Clamp(rotationVector.x, 0, 1f);
        rotationVector.y = Mathf.Clamp(rotationVector.y, 0, 1f);
        Debug.Log(rotationVector);
        // camera.Rotate(rotationVector.x, rotationVector.y, 0);
        camera.eulerAngles = rotationVector;


        //TODO:
        /*
            The rotation is not working at the moment for the camera. I believe that the solution is to store the rotation in a persistent
            variable in order to ensure that it is not being reset to zero every time that the mouse input is zero. This should solve the problem.

        */
    }

    void CalculateMovement(Vector3 jumpVelocity)
    {
        Vector3 movementVelocity = new Vector3(movementX * speedX, jumpVelocity.y, speedZ);
        rb.velocity = movementVelocity;
    }
    void FixedUpdate()
    {
        Vector3 movementVelocity = new Vector3();
        if(jumpTime > 0)
        {
            if(jumpTime >= 10) speedY = 0.0002f;
            else speedY = 0.0003f;
            movementVelocity.y += Mathf.Sqrt(speedY * -3.0f * gravity);
            movementVelocity.y *= 100;
            jumpTime -= 1;
        }
        else
        {
            movementVelocity.y = 0;
        }
        CalculateMovement(movementVelocity);
        if(health <= 0)
        {
            deadMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar();
    }
}