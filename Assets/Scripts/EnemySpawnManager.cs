using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab musuh
    public Transform spawnPoint; // Titik spawn musuh

    private int totalEnemiesDied = 0;
    private bool spawning = false;
    private bool playerWon = false; // Tambahkan variabel untuk memeriksa apakah pemain menang

    private void Start()
    {
        totalEnemiesDied = 0;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (totalEnemiesDied < 1 && !playerWon) // Ubah angka 5 sesuai dengan jumlah musuh yang ingin Anda tentukan
        {
            if (!spawning && spawnPoint != null)

            {
                spawning = true;
                yield return new WaitForSeconds(5f); // Menunggu sebelum spawn musuh berikutnya
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                spawning = false;
            }
            yield return null;
        }
    }

    // Panggil method ini saat musuh mati
    public void EnemyDied()
    {
        totalEnemiesDied++;

        if (totalEnemiesDied >= 5 && !playerWon)
        {
            playerWon = true; // Atur bahwa pemain menang
            // Anda bisa tambahkan logika atau pesan kemenangan di sini
            Debug.Log("Player wins!");
        }
    }
}
