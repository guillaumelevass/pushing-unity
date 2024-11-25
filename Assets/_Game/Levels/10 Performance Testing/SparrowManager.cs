using System.Runtime.CompilerServices;
using UnityEngine;

public class SparrowManager : MonoBehaviour
{
    public GameObject ringPrefab; 
    public int NumberOfRings = 10;
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
            RingLoss(NumberOfRings);
        }
    }
    
    public void RingLoss(int numberOfRings)
    {
        callCount++;
        animator?.SetTrigger("RingLost");
        
        for (int i = 0; i < NumberOfRings; i++)
        {
                GameObject ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
                Rigidbody rb = ring.GetComponent<Rigidbody>();
                Vector3 randomDirection = Random.insideUnitSphere;
               
        //
        //     // Introduce a simulated inefficiency (inefficient method call)
             
        //
            rb.AddForce(randomDirection * 10, ForceMode.Impulse);
            
            ExpensiveOperation();
        }
        
        Debug.Log("Ring loss: " + callCount);
    }

    private void ExpensiveOperation()
    {

        // for (int x = 0; x < 1000; x++)
        // {
        //     for (int y = 0; y < 1000; y++)
        //     {
        //         grid[x, y] = Random.Range(0, 10); // Randomly assigned weights
        //     }
        // }
    }
    
    
}
