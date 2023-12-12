using UnityEngine;

public class CannedFood : MonoBehaviour
{
    public int healthRecoveryAmount = 50;

    public void Consume()
    {
        Debug.Log("Health should increase by {healthRecoveryAmount}.");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Consume();
        }
    }
}
