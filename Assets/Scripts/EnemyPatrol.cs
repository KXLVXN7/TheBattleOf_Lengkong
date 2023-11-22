using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;
    public float kecepatanGerak;

    public Transform target;
    public Transform detection; // Direction 1
    public Transform detection2; // Direction 2
    public float jarakRaycast;

    public int maxPeluru = 10; // Jumlah maksimum peluru musuh
    private int peluruSisa; // Jumlah peluru yang tersisa

    public GameObject projectilePrefab; // Prefab peluru
    public Transform bulletSpawnPoint; // Titik keluarnya peluru

    private bool sedangMenungguPeluru = false; // Menyimpan apakah musuh sedang menunggu peluru

    private bool playerTerdeteksi = false; // Menyimpan apakah pemain terdeteksi
    private Transform playerTransform; // Menyimpan transform pemain
    public Animator enemyAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = detection; // Set initial target to detection
        peluruSisa = maxPeluru; // Set jumlah peluru awal
    }

    void Update()
    {
        if (sedangMenungguPeluru)
        {
            // Musuh sedang menunggu peluru, jangan gerakkan
            rb.velocity = Vector2.zero;
        }
        else
        {
            // Calculate the direction to the target point
            Vector2 dirToTarget = (target.position - transform.position).normalized;

            // Cek jarak antara musuh dan target
            if (Vector2.Distance(transform.position, target.position) < 1.5f)
            {
                // Musuh telah mencapai target, berhenti
                rb.velocity = Vector2.zero;

                if (peluruSisa > 0)
                {
                    // Menembak jika masih ada peluru
                    Shoot();

                    // Setelah menembak, kurangi jumlah peluru
                    peluruSisa--;

                    // Musuh selesai menunggu peluru
                    sedangMenungguPeluru = true;

                    // Set the "enemyshoot" parameter to trigger the attack animation
                    if (enemyAnimator != null)
                    {
                        enemyAnimator.SetBool("enemyshoot", true);
                    }
                }
                else
                {
                    // Jika peluru habis, ganti target
                    SwitchTarget();
                }
            }
            else
            {
                // Move towards the target using velocity
                rb.velocity = dirToTarget * kecepatanGerak;
            }

            // Deteksi pemain
            if (!playerTerdeteksi)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, jarakRaycast);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    playerTerdeteksi = true;
                    playerTransform = hit.collider.transform;
                    target = detection; // Musuh berhenti di detection saat melihat pemain
                }
            }
        }

        if (sedangMenungguPeluru && peluruSisa <= 0)
        {
            // Jika sedang menunggu peluru dan peluru sudah habis, lanjutkan perjalanan
            sedangMenungguPeluru = false;
            SwitchTarget();

            // Reset the "enemyshoot" parameter when not shooting
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("enemyshoot", false);
            }
        }
    }

    void SwitchTarget()
    {
        // Ganti target antara detection dan detection2
        if (target == detection)
        {
            target = detection2;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            target = detection;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void Shoot()
    {
        // Instantiate peluru dari prefab
        GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Atur arah gerakan peluru sesuai dengan arah musuh
        Projectiles bulletController = bullet.GetComponent<Projectiles>();
        if (bulletController != null)
        {
            Vector2 shootingDirection = (target.position - bulletSpawnPoint.position).normalized;
            bulletController.direction = shootingDirection;
        }

        // Set the "enemyshoot" parameter to trigger the attack animation
        if (enemyAnimator != null)
        {
            enemyAnimator.SetBool("enemyshoot", true);
        }
    }
}