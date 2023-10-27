using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isGrounded;
    private bool isJumping = false; // Tambah variabel ini
    private bool isCrouching = false; // Tambah variabel ini
    private float moveSpeed = 5f;
    public Animator anim;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent < SpriteRenderer>();
        anim = GetComponent < Animator>();
    }

    void Update()
    {
        // Gerak Horizontal KIRI/KANAN
        float moveX = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Gerakan Flip
        if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Rotasi 180 derajat (balik)
        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Gerakan Lompat
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isCrouching)
        {
            Debug.Log("Lompat");
            anim.SetBool("Lompat", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
        }
        else
        {
            anim.SetBool("Lompat", false);
        }

        if (Input.GetKey(KeyCode.S) && isGrounded && !isJumping)
        {
            anim.SetBool("Jongkok", true);
            isCrouching = true;
        }
        else
        {
            anim.SetBool("Jongkok", false);
            isCrouching = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek bersentuhan dengan tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ketika karakter meninggalkan tanah, isGrounded diatur ke false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
