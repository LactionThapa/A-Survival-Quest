using UnityEngine;

public class MP5Magazine : MonoBehaviour
{
    public int ammoAmount = 30; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MP5 mp5 = other.GetComponentInChildren<MP5>();
            if (mp5 != null)
            {
                mp5.AddAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}