using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;

    [SerializeField] private float speed;
    [SerializeField] private GameObject focalPoint;

    [Header("Powerups")]
    private bool hasPowerup = false;
    [SerializeField] private float powerUpStregth;
    [SerializeField] private GameObject powerupIndicator;

    public PowerUpType currentPowerUp = PowerUpType.None;

    [Header("Rocket Powerups")]
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    [Header("Smash Powerups")]
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    [SerializeField] private bool smashing = false;
    [SerializeField] private float floorY;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * forwardInput *  speed);


        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUP"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidBody.AddForce(awayFromPlayer * powerUpStregth, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " with the powerup set to " + currentPowerUp.ToString());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            tmpRocket = Instantiate(rocketPrefab,transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        //store the y position before taking off
        floorY = transform.position.y;

        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            //Move the player up while still keeping their x velocity
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, smashSpeed);
            yield return null;
        }

        //Now move the player Down
        while (transform.position.y > floorY)
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocity.x, -smashSpeed * 2);
            yield return null;
        }

        //Cycle through all the enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            //Apply an explosion force that originates from our position
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }

            //We are no longer smashing so set the boolean to false
            smashing = false;
        }
    }
}
