using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public Vector3 direction;
    public float maxRange = 3f; // Jarak maksimum proyektil

    private Vector3 initialPosition; // Menyimpan posisi awal proyektil
    public float playerDamage = 12f; // Definisi variabel damage untuk pemain
    public float enemyDamage = 8f; // Definisi variabel damage untuk musuh

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Menggerakkan proyektil
        transform.position += direction * Time.deltaTime * speed;

        // Mengecek apakah proyektil telah mencapai jarak maksimum
        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject); // Menghancurkan proyektil jika mencapai jarak maksimum
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
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
                Debug.Log("Darah Enemy berkurang" + enemyDamage);

                // Menghancurkan proyektil setelah menabrak musuh
                Destroy(gameObject);
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
                Debug.Log("Darah Player berkurang" + playerDamage);

                // Menghancurkan proyektil setelah menabrak pemain
                Destroy(gameObject);
            }
        }
        // Menghancurkan proyektil jika menabrak objek selain musuh, pemain, dan proyektil musuh
        else if (!collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            Destroy(gameObject);
        }
    }
}
