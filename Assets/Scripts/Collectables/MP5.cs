using UnityEngine;

public class MP5 : MonoBehaviour
{
    public float damage = 10f;  
    public float range = 100f;  
    public float fireRate = 10f;  
    public int maxAmmo = 30;  
    private int currentAmmo;  
    public int reserveAmmo = 90;  

    private float nextTimeToFire = 0f;  

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("No Ammo");
            }
        }

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
    }
}