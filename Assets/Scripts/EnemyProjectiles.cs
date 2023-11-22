using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    public float speed = 10f; // Kecepatan proyektil
    public Vector3 direction;
    public float maxRange = 3f; // Jarak maksimum proyektil

    private Vector3 initialPosition; // Menyimpan posisi awal proyektil
    public float playerDamage = 12f; // Variabel damage untuk pemain

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
        // Periksa apakah objek yang ditabrak adalah pemain (dengan tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // Dapatkan komponen Health dari objek pemain
            Health playerHealthComponent = collision.gameObject.GetComponent<Health>();

            // Periksa apakah objek yang ditabrak memiliki komponen Health
            if (playerHealthComponent != null)
            {
                // Serang pemain dengan damage yang sesuai
                playerHealthComponent.takeDamage(playerDamage);
                Debug.Log("Darah Player berkurang" + playerDamage);

                // Hancurkan proyektil setelah menabrak pemain
                Destroy(gameObject);
            }
        }
        // Hancurkan proyektil jika menabrak objek selain pemain
        else
        {
            Destroy(gameObject);
        }
    }
}
