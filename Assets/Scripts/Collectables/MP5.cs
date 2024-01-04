using UnityEngine;

public class MP5 : Weapon
{ 
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadTime(reloadTime));
        }
    }
}