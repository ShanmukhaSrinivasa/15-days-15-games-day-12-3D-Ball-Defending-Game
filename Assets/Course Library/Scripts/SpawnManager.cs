using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefabs;
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private float spawnRange;
    [SerializeField] private int enemyCount;
    [SerializeField] private int waveNumber = 1;

    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerUP();
        }
    }

    private void SpawnPowerUP()
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs, GenerateSpawnPosition(), enemyPrefabs.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnRangeX, 0, spawnRangeZ);
        return spawnPos;
    }
}
