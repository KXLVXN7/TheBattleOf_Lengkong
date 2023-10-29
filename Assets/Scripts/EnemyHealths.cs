using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealths : MonoBehaviour
{
    private float maxHPEnemy = 100;
    private float currentHPEnemy = 100;

    [SerializeField] private Image HPBarEnemy;

    private bool isDead = false;

    void Start()
    {
        currentHPEnemy = maxHPEnemy;
        UpdateHealthBars();
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
            currentHPEnemy = Mathf.Clamp(currentHPEnemy, 0, maxHPEnemy); // Pastikan kesehatan tidak kurang dari 0 atau melebihi maksimum
            UpdateHealthBars();
            StartCoroutine(VisualIndicators(Color.red));
            if (currentHPEnemy == 0)
            {
                Die();
            }

            /* //GetComponent<Animator>().SetTrigger("PlayerDead");
             Destroy(gameObject);*/

        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            // Tambahkan logika kematian pemain di sini, misalnya menampilkan pesan kematian, mengakhiri permainan, atau mengatur ulang level
            Debug.Log("Enemy mati!");
            Destroy(gameObject);
        }
    }
}
