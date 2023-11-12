using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float fireRate = 1.0f;
    public int maxBulletsPerMinute = 8;
    private int bulletsFired = 0;
    private float lastFireTime = 0.0f;
    public Text bulletText;
    public Text bulletReload;
    private bool isReloading = false;
    private float reloadTime = 1f;
    public Animator anim;
    [SerializeField] private AudioSource gunshotSFX;
    [SerializeField] private AudioSource reloadSFX;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanFire())
        {
            ShootProjectile();
            anim.SetBool("Nembak", true);
            gunshotSFX.Play();
        }
        else
        {
            anim.SetBool("Nembak", false);
        }

        if (bulletsFired >= maxBulletsPerMinute)
        {
            if (Time.time - lastFireTime >= 60.0f)
            {
                bulletsFired = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) &&!isReloading  &&bulletsFired > 0)
        {
            StartReload();
            //anim.SetBool("reload", true);
            reloadSFX.Play();
        }
        else
        {
            //anim.SetBool("reload", false);
        }
    }

    bool CanFire()
    {
        if (bulletsFired >= maxBulletsPerMinute) return false;
        if (Time.time - lastFireTime < 1.0f / fireRate) return false;
        return true;
    }

    void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Projectiles projectileScript = newProjectile.GetComponent<Projectiles>();
        if (projectileScript != null)
        {
            projectileScript.direction = spawnPoint.transform.right;
            lastFireTime = Time.time;
            bulletsFired++;
            UpdateBulletCounter();
        }
    }

    void UpdateBulletCounter()
    {
        if (bulletText != null)
        {
            bulletText.text = ": " + (maxBulletsPerMinute - bulletsFired).ToString();
        }
    }

    void StartReload()
    {
        if (!isReloading)
        {
            isReloading = true;
            bulletsFired = 0;
            lastFireTime = Time.time;
            StartCoroutine(ReloadCoroutine());

            anim.SetBool("reload", true);
        }
    }

    IEnumerator ReloadCoroutine()
    {
        float remainingTime = reloadTime;

        while (remainingTime > 0f)
        {
            UpdateReloadText(remainingTime);
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        isReloading = false;
        UpdateBulletCounter();

        anim.SetBool("reload", false);
    }

    void UpdateReloadText(float remainingTime)
    {
        if (bulletReload != null)
        {
            int displayedTime = Mathf.Max(Mathf.FloorToInt(remainingTime), 0);

            // Check if the remaining time is zero, then display "Reload Cooldown: 0"
            if (displayedTime == 0)
            {
                bulletReload.text = "Press R to Reload";
            }
            else
            {
                bulletReload.text = "Reloading" + displayedTime.ToString();
            }
            //bulletReload.text = "Reload Cooldown: " + Mathf.CeilToInt(remainingTime).ToString();
        }
    }
}
