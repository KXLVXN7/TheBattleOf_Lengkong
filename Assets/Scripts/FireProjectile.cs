using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab proyektil
    public Transform spawnPoint; // Titik keluar proyektil
    public float fireRate = 1.0f; // Jangka waktu antara setiap peluru (dalam detik)
    public int maxBulletsPerMinute = 32; // Jumlah peluru maksimum dalam 1 menit
    private int bulletsFired = 0; // Jumlah peluru yang sudah ditembakkan
    private float lastFireTime = 0.0f;
    public Text bulletText; // Referensi ke komponen UI Text
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanFire())
        {
            ShootProjectile();
        }

        if (bulletsFired >= maxBulletsPerMinute)
        {
            // Menunggu 1 menit sebelum mengatur ulang jumlah peluru yang ditembakkan
            if (Time.time - lastFireTime >= 60.0f)
            {
                bulletsFired = 0;
            }
        }
    }

    bool CanFire()
    {
        if (bulletsFired >= maxBulletsPerMinute) return false; // Jika sudah mencapai batas peluru
        if (Time.time - lastFireTime < 1.0f / fireRate) return false; // Jika belum mencapai tingkat tembakan yang diinginkan

        return true;
    }

    void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Projectiles projectileScript = newProjectile.GetComponent<Projectiles>();
        if (projectileScript != null)
        {
            // Setelah proyektil dibangkitkan, Anda dapat menyesuaikan arah atau kecepatan proyektil
            projectileScript.direction = spawnPoint.transform.right;

            // Catat waktu terakhir peluru ditembakkan dan tambah jumlah peluru yang sudah ditembakkan
            lastFireTime = Time.time;
            bulletsFired++;

            // Perbarui teks pada UI Text
            UpdateBulletCounter();
        }
    }
    void UpdateBulletCounter()
    {
        // Pastikan bulletText telah diatur di inspektor
        if (bulletText != null)
        {
            bulletText.text = "Ammo: " + (maxBulletsPerMinute - bulletsFired).ToString();
        }
    }
}
