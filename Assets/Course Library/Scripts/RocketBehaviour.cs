using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool homing = false;

    private float rocketStrength = 15.0f;
    private float aliveTimer = 5.0f;

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;

            transform.position += moveDirection * speed * Time.deltaTime;

            transform.LookAt(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
         This method first checks if we have a target. If we do, we compare the tag of the colliding object with
         the tag of the target. If they match, we get the rigidbody of the target. We then use the normal of the
         collision contact to determine which direction to push the target in. Finally we apply the force to the
         target and destroy the missile.
        */

        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidBody = target.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRigidBody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
