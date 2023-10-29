using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHP = 100;
    private float currentHP = 100;

    [SerializeField] private Image HPBar;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        HPBar.fillAmount = currentHP / maxHP;
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.35f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void takeDamage(float damage)
    {
        if (!isDead)
        {
            currentHP -= damage;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP); // Pastikan kesehatan tidak kurang dari 0 atau melebihi maksimum
            UpdateHealthBar();
            StartCoroutine(VisualIndicator(Color.red));
            if (currentHP == 0)
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
            // Tambahkan logika kematian pemain di sini, misalnya menampilkan pesan kematian, mengakhiri permainan, atau mengatur ulang level
            Debug.Log("Player mati!");
            Destroy(gameObject);
        }
    }
}
