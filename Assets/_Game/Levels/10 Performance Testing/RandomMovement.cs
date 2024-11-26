using UnityEngine;

public class RandomMovementXZ : MonoBehaviour
{
    private const int MAX_DISTANCE = 5;
    public float rotationSpeed = 5f; // Speed at which the character rotates
    public float speed = 2f; // Speed at which the character moves
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector3 previousPosition;
    private float stuckThreshold = 0.01f; // Threshold to determine if position has changed

    void Start()
    {
        // Initialize character's starting position randomly within the grid
        initialPosition = new Vector3(Random.Range(0, MAX_DISTANCE), 0, Random.Range(0, MAX_DISTANCE));
        transform.position = initialPosition;

        // Set the initial target position
        SetNewTargetPosition();
    }

    void Update()
    {
        // Move incrementally towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        // Rotate to face the target position
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        // Check if the character has reached the target position
        if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Redefine a new target position within 10 units from the initial starting position
            SetNewTargetPosition();
        }
        
        // Check if the character is stuck (position hasn't changed significantly since the last update)
        if (Vector3.Distance(transform.position, previousPosition) < stuckThreshold)
        {
            Debug.Log("Character is stuck, setting new target position.");
            SetNewTargetPosition();
        }

        // Update the previous position for the next frame
        previousPosition = transform.position;
    }

    void SetNewTargetPosition()
    {
        float randomX = Random.Range(initialPosition.x - MAX_DISTANCE, initialPosition.x + MAX_DISTANCE);
        float randomZ = Random.Range(initialPosition.z - MAX_DISTANCE, initialPosition.z + MAX_DISTANCE);

        // Clamp the new target position within the defined boundaries
        randomX = Mathf.Clamp(randomX, initialPosition.x - MAX_DISTANCE, initialPosition.x + MAX_DISTANCE);
        randomZ = Mathf.Clamp(randomZ, initialPosition.z - MAX_DISTANCE, initialPosition.z + MAX_DISTANCE);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }
}