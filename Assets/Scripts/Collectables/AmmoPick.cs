using UnityEngine;

public class AmmoPick : MonoBehaviour
{
    public int ammoAmount = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Glock glock = other.GetComponentInChildren<Glock>();
            if (glock != null)
            {
                glock.AddAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
