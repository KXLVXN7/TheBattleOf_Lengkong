using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public Vector3 direction;
    public float maxRange = 3f; // Jarak maksimum proyektil

    private Vector3 initialPosition; // Menyimpan posisi awal proyektil
    public float playerDamage = 8f; // Definisi variabel damage untuk pemain
    public float enemyDamage = 10f; // Definisi variabel damage untuk musuh

    public float throwForce = 10f;
    public float fuseTime = 3f;
    private Animator animator;

    void Start()
    {

        initialPosition = transform.position;
/*        SetDirection(Vector3.right);*/
        StartCoroutine(ExplodeAfterDelay());
        animator = GetComponent<Animator>();
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    void Explode()
    {
        

        // Find all colliders in the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, maxRange);

        foreach (Collider2D collider in colliders)
        {
            // Apply explosion force to rigidbodies
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((collider.transform.position - transform.position).normalized * throwForce, ForceMode2D.Impulse);
            }

            // Deal damage to enemies or players within the explosion radius
            if (collider.CompareTag("Enemy"))
            {
                EnemyHealths enemyHealth = collider.GetComponent<EnemyHealths>();
                if (enemyHealth != null)
                {
                    enemyHealth.takeDamage(enemyDamage); // Adjust damage as needed
                    animator.SetBool("explode", true);
                }
            }
            else if (collider.CompareTag("Player"))
            {
                Health playerHealth = collider.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.takeDamage(playerDamage); // Adjust damage as needed
                }
            }
        }

        // Optionally, you can instantiate particle effects or play a sound here

        // Destroy the grenade after the explosion
        Destroy(gameObject);
    }

/*    void Update()
    {
        // Menggerakkan proyektil
        transform.position += direction * Time.deltaTime * speed;

        // Mengecek apakah proyektil telah mencapai jarak maksimum
        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);
        if (distanceTraveled >= maxRange)
        {
            Explode(); // Call the explosion method when reaching the max range
        }
        else
        {
            // Menggerakkan proyektil
            transform.position += direction * Time.deltaTime * speed;
        }
    }*/

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
        Debug.Log("Direction set to: " + newDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek apakah objek yang ditabrak adalah musuh (dengan tag "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Mengambil komponen EnemyHealths dari objek yang ditabrak
            EnemyHealths enemyHealthComponent = collision.gameObject.GetComponent<EnemyHealths>();

            // Mengecek apakah objek yang ditabrak memiliki komponen EnemyHealths
            if (enemyHealthComponent != null)
            {
                // Menyerang musuh dengan damage yang sesuai
                enemyHealthComponent.takeDamage(enemyDamage);

                // Delay the explosion animation
                StartCoroutine(ExplodeWithDelay());


                // Menghancurkan proyektil setelah menabrak musuh
                //Destroy(gameObject);
            }
        }
        // Mengecek apakah objek yang ditabrak adalah pemain (dengan tag "Player")
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Mengambil komponen Health dari objek pemain
            Health playerHealthComponent = collision.gameObject.GetComponent<Health>();

            // Mengecek apakah objek yang ditabrak memiliki komponen Health
            if (playerHealthComponent != null)
            {
                // Menyerang pemain dengan damage yang sesuai
                playerHealthComponent.takeDamage(playerDamage);

                // Delay the explosion animation
                StartCoroutine(ExplodeWithDelay());


                // Menghancurkan proyektil setelah menabrak pemain
                //Destroy(gameObject);
            }
        }
        // Menghancurkan proyektil jika menabrak objek selain musuh, pemain, dan proyektil musuh
        else if (!collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ExplodeWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay duration as needed

        if (animator != null)
        {
            animator.SetTrigger("explode");
        }

        Destroy(gameObject);
    }
}
