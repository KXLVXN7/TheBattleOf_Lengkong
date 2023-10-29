using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float maxHP = 100;
    private float currentHP = 100;

    [SerializeField] private Image HPBar;

    private EnemySpawnManager spawnManager; // Referensi ke EnemySpawnManager

    void Awake()
    {
        //HPBar = GetComponent<Image>();
        spawnManager = FindObjectOfType<EnemySpawnManager>(); // Temukan instance EnemySpawnManager
    }

    void Start()
    {
        currentHP = maxHP;
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.35f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage()
    {
        currentHP -= 5;
        HPBar.fillAmount = currentHP / maxHP;
        StartCoroutine(VisualIndicator(Color.red));
        Death();
    }

    public void Death()
    {
        if (currentHP <= 0)
        {
            // Panggil method untuk menghitung musuh yang mati
            spawnManager.EnemyDied();
            
            Destroy(gameObject);
        }
    }
}
