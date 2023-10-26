using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isGrounded;
    private float moveSpeed = 5f;
/*    [SerializeField] private float jumpForce = 8f;*/
    [SerializeField] private Transform Player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.position, Time.deltaTime);
        
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek bersentuhan dengan tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Ketika karakter meninggalkan tanah, isGrounded diatur ke false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }*/
    }
