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
    public int maxBulletsPerMinute = 50; // Jumlah peluru maksimum dalam 1 menit
    private int bulletsFired = 0; // Jumlah peluru yang sudah ditembakkan
    private float lastFireTime = 0.0f;
    public Text bulletText; // Referensi ke komponen UI Text
    public Text bulletReload;
    private bool isReloading = false;
    private float reloadTime = 5f; // Time it takes to reload in seconds
    public Animator anim;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanFire())
        {
            ShootProjectile();
            anim.SetBool("Nembak", true);
        }
        else
        {
            anim.SetBool("Nembak", false);
        }

        if (bulletsFired >= maxBulletsPerMinute)
        {
            // Menunggu 1 menit sebelum mengatur ulang jumlah peluru yang ditembakkan
            if (Time.time - lastFireTime >= 60.0f)
            {
                bulletsFired = 0;
            }
        }
        // Check if the player wants to reload (for example, when pressing the "R" key)
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && bulletsFired > 0)
        {
            StartReload();
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

            // Perbarui teks pada UI Text untuk informasi amunisi dan waktu reloading
            UpdateBulletCounter();
        }
    }

    void UpdateBulletCounter()
    {
        // Pastikan bulletText telah diatur di inspektor
        if (bulletText != null)
        {
            // Hitung waktu per reloading satu peluru dalam detik
            float timePerBulletReload = 60.0f / maxBulletsPerMinute;

            // Hitung waktu yang tersisa hingga peluru terisi kembali
            float timeToReload = timePerBulletReload * (maxBulletsPerMinute - bulletsFired);

            // Ubah waktu reloading ke format yang sesuai (misalnya, menit:detik)
            int minutes = Mathf.FloorToInt(timeToReload / 60);
            int seconds = Mathf.FloorToInt(timeToReload % 60);
            bulletText.text = "Ammo: " + (maxBulletsPerMinute - bulletsFired).ToString();
            bulletReload.text = "Reloading: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        }
    }

    void StartReload()
    {
        isReloading = true;
        bulletsFired = 0;
        lastFireTime = Time.time;


        // Start a coroutine to handle the reloading time
        StartCoroutine(ReloadCoroutine());
    }
    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);

        // Reload is complete
        isReloading = false;

        // Update UI and reset reload animation
        UpdateBulletCounter();
    }
}
