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
                Die();
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            killCount++; // Menambahkan 1 ke hitungan kill
            enemyDied.text = ": " + killCount; // Memperbarui teks dengan jumlah kill
            Debug.Log("Enemy mati! Total Kills: " + killCount);
            Destroy(gameObject);
        }
    }

    public void ResetKillCount()
    {
        killCount = initialKillCount; // Mereset skor ke nilai awal
        enemyDied.text = ": " + killCount; // Memperbarui teks dengan jumlah kill yang telah direset
    }
}
