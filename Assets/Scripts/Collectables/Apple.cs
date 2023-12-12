using UnityEngine;

public class Apple : MonoBehaviour
{
    public int healthRecoveryAmount = 15;  

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            Consume();
        }
    }

    void Consume()
    {
        Debug.Log("Health should increase by {healthRecoveryAmount}.");
        Destroy(gameObject);
    }
}
