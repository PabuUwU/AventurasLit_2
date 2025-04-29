using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movimiento : MonoBehaviour
{
    public float speed = 5f; // Velocidad normal
    public float runSpeed = 10f; // Velocidad al correr
    public float jumpForce = 10f; // Fuerza del salto
    public Transform groundCheck; // Punto para verificar el suelo
    public LayerMask groundLayer; // Capa del suelo

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGrounded;
    private BoxCollider2D boxCollider;
    private Vector2 originalOffset;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalOffset = boxCollider.offset;

    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float moveSpeed = isRunning ? runSpeed : speed;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Voltear sprite y ajustar hitbox
        if (moveInput > 0)
        {
            sr.flipX = false;
            boxCollider.offset = new Vector2(originalOffset.x, originalOffset.y);
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
            boxCollider.offset = new Vector2(-originalOffset.x, originalOffset.y);
        }

        // Detectar si el personaje está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Saltar solo si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}