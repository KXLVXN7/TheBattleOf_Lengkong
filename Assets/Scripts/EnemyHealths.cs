using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealths : MonoBehaviour
{
    private float maxHPEnemy = 100;
    private float currentHPEnemy = 100;
    public Text enemyDied;
    public Animator anim;
    public GameObject healthBagPrefab;

    [SerializeField] private Image HPBarEnemy;

    private bool isDead = false;
    private static int killCount = 0; // Menambahkan variabel untuk menghitung jumlah kill

    private int initialKillCount = 0; // Skor awal yang dapat digunakan untuk mereset skor

    void Start()
    {
        currentHPEnemy = maxHPEnemy;
        UpdateHealthBars();
        initialKillCount = killCount; // Inisialisasi skor awal untuk reset
    }

    private void UpdateHealthBars()
    {
        HPBarEnemy.fillAmount = currentHPEnemy / maxHPEnemy;
    }

    private IEnumerator VisualIndicators(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.35f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    public void takeDamage(float damage)
    {
        if (!isDead && !gameObject.CompareTag("ProjectileEnemy"))
        {
            currentHPEnemy -= damage;
            currentHPEnemy = Mathf.Clamp(currentHPEnemy, 0, maxHPEnemy);
            UpdateHealthBars();
            StartCoroutine(VisualIndicators(Color.red));
            if (currentHPEnemy == 0)
            {
                DieWithAnimation();
            }
        }
    }

    private void DieWithAnimation()
    {
        if (!isDead)
        {
            isDead = true;
            killCount++; // Menambahkan 1 ke hitungan kill
            enemyDied.text = ": " + killCount; // Memperbarui teks dengan jumlah kill
            Debug.Log("Enemy mati! Total Kills: " + killCount);
            anim.SetBool("enemyDeath", true);

            // Hancurkan objek setelah animasi selesai (gantilah "AnimationDuration" dengan durasi animasi yang benar)
            StartCoroutine(DestroyAfterAnimation());
            // healthbag next
            // Hitung jumlah kesehatan yang akan dijatuhkan (10-15% dari maxHealth)
            int healthToDrop = Mathf.RoundToInt(Random.Range(0.1f, 0.15f) * maxHPEnemy);

            // Instantiate objek HealthBag di posisi musuh dengan jumlah kesehatan yang dihitung
            GameObject healthBag = Instantiate(healthBagPrefab, transform.position, Quaternion.identity);
            /*healthBag.GetComponent<HealthBagScript>().SetHealthAmount(healthToDrop);*/
            // Instantiate objek HealthBag di posisi musuh
            Instantiate(healthBagPrefab, transform.position, Quaternion.identity);
            // Hancurkan musuh
            /*Destroy(gameObject);*/
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        float animationDuration = 0.3f;/*gantilah dengan durasi animasi yang benar*/;
        yield return new WaitForSeconds(animationDuration);
        Destroy(gameObject);
    }

    public void ResetKillCount_NewGame()
    {
        killCount = 0; // Mereset skor ke nilai awal
        enemyDied.text = ": " + killCount; // Memperbarui teks dengan jumlah kill yang telah direset
    }
}