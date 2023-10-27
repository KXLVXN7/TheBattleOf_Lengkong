using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemyProjectilePrefab; // Prefab proyektil musuh
    public Transform enemySpawnPoint; // Titik keluar proyektil musuh
    public float enemyFireRate = 1.0f; // Laju tembak musuh (peluru per detik)
    public int enemyMaxBulletsPerMinute = 24; // Jumlah maksimum peluru per menit musuh
    public Transform player; // Referensi ke pemain

    private int enemyBulletsFired = 0; // Jumlah peluru yang sudah ditembakkan oleh musuh
    private float enemyLastFireTime = 0.0f;
    public float maxAimError = 10.0f; // Maksimum kesalahan dalam arah tembakan
    private bool isReloading = false; // Apakah sedang reload
    private float reloadStartTime = 0.0f; // Waktu awal reload
    public float reloadCooldown = 3.0f; // Waktu cooldown reload (3 detik)

    void Update()
    {
        if (player == null)
        {
            return; // Pastikan pemain ada sebelum menembak
        }

        if (CanEnemyFire() && !isReloading)
        {
            Debug.Log("Enemy is shooting");
            // Hitung arah tembakan ke pemain dengan sedikit randomness
            Vector3 directionToPlayer = (player.position - enemySpawnPoint.position).normalized;
            float randomError = Random.Range(-maxAimError, maxAimError);
            directionToPlayer = Quaternion.Euler(0, 0, randomError) * directionToPlayer;

            // Tembak proyektil dengan arah yang sudah dihitung
            ShootProjectile(enemySpawnPoint, enemyProjectilePrefab, ref enemyBulletsFired, ref enemyLastFireTime, directionToPlayer);

            // Mulai cooldown reload setelah menembak
            isReloading = true;
            reloadStartTime = Time.time;
        }

        // Cek apakah cooldown reload sudah selesai
        if (isReloading && Time.time - reloadStartTime >= reloadCooldown)
        {
            isReloading = false;
        }
    }

    bool CanEnemyFire()
    {
        if (enemyBulletsFired >= enemyMaxBulletsPerMinute) return false;
        if (Time.time - enemyLastFireTime < 1.0f / enemyFireRate) return false;

        return true;
    }

    void ShootProjectile(Transform spawnPoint, GameObject projectilePrefab, ref int bulletsFired, ref float lastFireTime, Vector3 shootingDirection)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Projectiles projectileScript = newProjectile.GetComponent<Projectiles>();
        if (projectileScript != null)
        {
            // Atur arah proyektil sesuai dengan shootingDirection
            projectileScript.direction = shootingDirection;

            // Catat waktu terakhir peluru ditembakkan dan tambah jumlah peluru yang sudah ditembakkan
            lastFireTime = Time.time;
            bulletsFired++;
        }
        Debug.Log("Shooting at direction: " + shootingDirection);
    }
}