using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class FlickyManager : MonoBehaviour
{
    public GameObject babyPrefab; 
    public int NumberOfChirpies = 50;
    public bool MoreAwesomeAILogic = false;
    private int callCount;

    private int[,] grid = new int[1000, 1000];

    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoseChirpies(NumberOfChirpies);
        }
    }
    
    public void LoseChirpies(int numberOfRings)
    {
        animator?.SetTrigger("Hit");
        
        for (int i = 0; i < NumberOfChirpies; i++)
        {
            GameObject ring = Instantiate(babyPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = ring.GetComponent<Rigidbody>();
            Vector3 randomDirection = Random.insideUnitSphere;
            rb.AddForce(randomDirection * 10, ForceMode.Impulse);
            if (MoreAwesomeAILogic)
            {
                ExpensiveCPUOperations();
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the specific object you are interested in
        if (collision.gameObject.CompareTag("enemy"))
        {
            // Call your method when characters touch
            LoseChirpies(NumberOfChirpies);
        }
    }
    
    private void ExpensiveCPUOperations()
    {
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                grid[x, y] = Random.Range(0, 10); 
            }
        }
    }
}
