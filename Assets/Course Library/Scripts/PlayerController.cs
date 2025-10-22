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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUP"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            enemyRigidBody.AddForce(awayFromPlayer * powerUpStregth, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " with the powerup set to " + hasPowerup);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
