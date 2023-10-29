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
            Debug.Log("Player mati!");
            // Mengatur jeda selama 4 detik
            StartCoroutine(DeathPauseAndShowUI());
        }
    }

    private IEnumerator DeathPauseAndShowUI()
    {
        // Set Time.timeScale ke 0 untuk menghentikan permainan
        Dead.SetActive(true); // Gantilah yourDeathUI dengan objek UI kematian yang sesuai
        Time.timeScale = 0f;

        // Jeda selama 4 detik
        yield return new WaitForSeconds(4.0f);

        // Tampilkan UI atau lakukan tindakan lain, contoh:
       

        // Selanjutnya, Anda dapat menambahkan logika lain, seperti mengakhiri permainan atau mengatur ulang level

        // Set Time.timeScale kembali ke 1 untuk melanjutkan permainan
/*        Time.timeScale = 1f;
*/    }

}
