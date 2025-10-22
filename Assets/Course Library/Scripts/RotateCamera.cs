using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    void Start()
    {
        
    }

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput *  rotationSpeed * Time.deltaTime); 
    }
}
