using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public Vector3 direction;
    public float maxRange = 3f; // Jarak maksimum proyektil

    private Vector3 initialPosition; // Menyimpan posisi awal proyektil
    private float damage = 8f; // Definisi variabel damage.
    private float enemyDamage = 3f;

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
            // Mengambil komponen Health dari objek yang ditabrak
            Health healthComponent = collision.gameObject.GetComponent<Health>();

            // Mengambil komponen EnemyHealths dari objek yang ditabrak
            EnemyHealths enemyHealthComponent = collision.gameObject.GetComponent<EnemyHealths>();

            // Mengecek apakah objek yang ditabrak memiliki komponen Health
            if (enemyHealthComponent != null)
            {
                // Menyerang musuh dengan damage yang sesuai
                enemyHealthComponent.takeDamage(damage);
            }
            else if (healthComponent != null)
            {
                // Menyerang musuh dengan damage yang sesuai
                healthComponent.takeDamage(enemyDamage);
            }

            // Menghancurkan proyektil setelah menabrak musuh
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            // Menghancurkan proyektil jika menabrak objek selain musuh dan proyektil musuh
            Destroy(gameObject);
        }
    }
}
