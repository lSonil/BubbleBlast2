using UnityEngine;

public class OrbitingProjectile : MonoBehaviour
{
    private Transform target;  // The object to orbit (e.g., the player)
    public float orbitRadius = 2.0f;
    public float orbitSpeed = 1.0f;

    private Vector3 initialPosition;
    private float currentAngle = 0.0f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // Store the initial position relative to the target
        initialPosition = transform.position - target.position;
    }

    private void Update()
    {
        // Update the angle of rotation
        currentAngle += orbitSpeed * Time.deltaTime;

        // Calculate the new position based on the angle and radius
        Vector3 orbitPosition = new Vector3(
            Mathf.Cos(currentAngle) * orbitRadius,
            Mathf.Sin(currentAngle) * orbitRadius,
            0
        );

        // Update the position of the projectile relative to the target
        transform.position = target.position + orbitPosition + initialPosition;
    }
}
