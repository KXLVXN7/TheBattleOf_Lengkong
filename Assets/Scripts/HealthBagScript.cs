using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBagScript : MonoBehaviour
{
    public int healthAmount = 4;

    private void Update()
    {
        // object bergerak ke bawah setiap frame
/*        transform.Translate(Vector3.down * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }*/
    }

    public void SetHealthAmount(int amount)
    {
        healthAmount = amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.tag);

        // Jika pemain menyentuh objek HealthBag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            // Tambahkan health pemain jika ada komponen Health
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                Debug.Log("Healing player!");
                playerHealth.Heal(healthAmount);
            }

            // Hancurkan objek HealthBag
            Destroy(gameObject);
        }
    }

}
