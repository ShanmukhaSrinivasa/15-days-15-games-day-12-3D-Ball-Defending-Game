using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] powerupPrefabs;
    [SerializeField] private float spawnRange;
    [SerializeField] private int enemyCount;
    [SerializeField] private int waveNumber = 1;

    [Header("Boss Info")]
    [SerializeField] private GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

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

            //Spawn a boss Every X Number of Waves
            if (waveNumber % bossRound == 0)
            {
                SPawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
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

    private void SPawnBossWave(int currentRound)
    {
        int miniEnemiesToSpawn;

        if (bossRound!= 0)
        {
            miniEnemiesToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemiesToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemyCount = miniEnemiesToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMiniEnemyIndex = Random.Range(0, miniEnemyPrefabs.Length);

            Instantiate(miniEnemyPrefabs[randomMiniEnemyIndex], GenerateSpawnPosition(), miniEnemyPrefabs[randomMiniEnemyIndex].transform.rotation);
        }
    }
}
