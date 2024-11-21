using UnityEngine;

public class SparrowManager : MonoBehaviour
{
    public GameObject ringPrefab; 
    public int NumberOfRings = 10;

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
        
        animator?.SetTrigger("RingLost");
        
        for (int i = 0; i < NumberOfRings; i++)
        {
                GameObject ring = Instantiate(ringPrefab, transform.position, Quaternion.identity);
                Rigidbody rb = ring.GetComponent<Rigidbody>();
                Vector3 randomDirection = Random.insideUnitSphere;
        //
        //     // Introduce a simulated inefficiency (inefficient method call)
        //     ExpensiveOperation();
        //
        rb.AddForce(randomDirection * 10, ForceMode.Impulse);
        }
    }
}
