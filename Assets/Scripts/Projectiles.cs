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
        if (!collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            Health healthComponent = collision.gameObject.GetComponent<Health>();
            EnemyHealths EnemyhealthComponent = collision.gameObject.GetComponent<EnemyHealths>();

            if (EnemyhealthComponent != null)
            {
                EnemyhealthComponent.takeDamage(damage);
            }
            else if (healthComponent != null)
            {
                healthComponent.takeDamage(enemyDamage);
            }
            else
            {
                return;
            }
        }

        /*        if (healthComponent != null)
                {
                    healthComponent.takeDamage(damage);
                }*/
        Destroy(gameObject);
    }
}
