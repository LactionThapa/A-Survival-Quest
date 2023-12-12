using UnityEngine;

public class Orange : MonoBehaviour
{
    public int healthRecoveryAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // other.GetComponent<PlayerHealth>().Heal(healthRecoveryAmount);


            Debug.Log("Picked up an orange and recovered health.");

            Destroy(gameObject);
        }
    }
}
