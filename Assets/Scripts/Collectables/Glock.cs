using UnityEngine;

public class Glock : Weapon
{

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
                if (canShoot)
                StartCoroutine(DelayBeforeShoot(fireRate));
            }
            else
            {
                Debug.Log("No Ammo");
            }
        }

        // Reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadTime(reloadTime));
        }
    }

}
