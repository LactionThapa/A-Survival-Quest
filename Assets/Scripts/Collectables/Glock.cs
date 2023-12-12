using UnityEngine;

public class Glock : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public int maxAmmo = 15; 
    public int currentAmmo; 
    public int reserveAmmo = 45; 

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update() 
    {
        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("No Ammo");
            }
        }

        // Reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // hit.transform.GetComponent<Health>().TakeDamage(damage);
        }
    }

    void Reload()
    {
        if (reserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo);

            currentAmmo += ammoToReload;
            reserveAmmo -= ammoToReload;

        }
        else if (reserveAmmo <= 0)
        {
            Debug.Log("No Ammo");
        }
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        Debug.Log("Added " + amount + " ammo. Current reserve ammo: " + reserveAmmo);
    }

}
