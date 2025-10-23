using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] powerupPrefabs;
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
        int randomPowerUpIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerUpIndex], GenerateSpawnPosition(), powerupPrefabs[randomPowerUpIndex].transform.rotation);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[randomEnemyIndex], GenerateSpawnPosition(), enemyPrefabs[randomEnemyIndex].transform.rotation);
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
