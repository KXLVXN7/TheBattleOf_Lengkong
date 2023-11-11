using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHP = 100;
    private float currentHP = 100;
    public GameObject Dead;
    [SerializeField] private Image HPBar;
    public static bool GameIsPaused = false;

    private bool isDead = false;
    public Animator anim;

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        Debug.Log("Player menerima Health,Current Health: " + currentHP);
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
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
            UpdateHealthBar();
            StartCoroutine(VisualIndicator(Color.red));
            if (currentHP <= 0)
            {
                Debug.Log("MATI KOCAK");
                Die();
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Player mati!");
            anim.SetBool("playerDeath", true);
            StartCoroutine(DeathPauseAndShowUI());
        }
    }

    private IEnumerator DeathPauseAndShowUI()
    {
        Dead.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSeconds(3.0f);
        // Di sini Anda dapat menambahkan logika lain, seperti mengakhiri permainan atau mengatur ulang level
        Time.timeScale = 1f;
    }
}
