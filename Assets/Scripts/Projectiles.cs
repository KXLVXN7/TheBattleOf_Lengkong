using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public Vector3 direction;
    public float maxRange = 3f; // Jarak maksimum proyektil

    private Vector3 initialPosition; // Menyimpan posisi awal proyektil
    public float enemyDamage = 8f; // Variabel damage untuk musuh

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Bergerakkan proyektil
        transform.position += direction * Time.deltaTime * speed;

        // Periksa apakah proyektil telah mencapai jarak maksimum
        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject); // Hancurkan proyektil jika mencapai jarak maksimum
        }
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Periksa apakah objek yang ditabrak adalah musuh (dengan tag "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Dapatkan komponen EnemyHealths dari objek yang ditabrak
            EnemyHealths enemyHealthComponent = collision.gameObject.GetComponent<EnemyHealths>();

            // Periksa apakah objek yang ditabrak memiliki komponen EnemyHealths
            if (enemyHealthComponent != null)
            {
                // Serang musuh dengan damage yang sesuai
                enemyHealthComponent.takeDamage(enemyDamage);
                Debug.Log("Darah Enemy berkurang" + enemyDamage);

                // Hancurkan proyektil setelah menabrak musuh
                Destroy(gameObject);
            }
        }
        // Hancurkan proyektil jika menabrak objek selain musuh
        else
        {
            Destroy(gameObject);
        }
    }
}
