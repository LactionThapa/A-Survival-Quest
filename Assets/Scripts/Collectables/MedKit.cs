using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int healthRecoveryAmount = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Use();
        }
    }

    public void Use()
    {
        Debug.Log("Health should increase by {healthRecoveryAmount}.");
        Destroy(gameObject);
    }
}
