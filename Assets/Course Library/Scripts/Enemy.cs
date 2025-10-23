using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRB;

    [Header("Boss Info")]
    public bool isBoss = false;

    public float spawnInterval;
    [SerializeField] private float nextSpawnTime;

    public int miniEnemyCount;

    private SpawnManager spawnManager;

    [SerializeField] private float speed;
    private GameObject player;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnManager = FindFirstObjectByType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);

        if (isBoss)
        {
            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemyCount);
            }
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
