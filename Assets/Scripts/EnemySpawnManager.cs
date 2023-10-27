using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab musuh
    public Transform spawnPoint; // Titik spawn musuh

    private int totalEnemiesSpawned = 0;
    private int totalEnemiesDied = 0;

    private void Start()
    {
        totalEnemiesSpawned = 0;
        totalEnemiesDied = 0;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (totalEnemiesDied < 5) // Ubah angka 5 sesuai dengan jumlah musuh yang ingin Anda tentukan
        {
            // Spawn musuh di titik spawnPoint
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Tambahkan jumlah totalEnemiesSpawned
            totalEnemiesSpawned++;

            yield return new WaitForSeconds(2f); // Atur waktu spawning sesuai kebutuhan
        }
    }

    // Panggil method ini saat musuh mati
    public void EnemyDied()
    {
        totalEnemiesDied++;

        if (totalEnemiesDied >= 5)
        {
            // Anda bisa tambahkan logika atau pesan kemenangan di sini
            Debug.Log("Player wins!");
        }
    }
}
