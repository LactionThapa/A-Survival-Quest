using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected int currentAmmo;
    [SerializeField] protected int Damage;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected float range = 100f;
    [SerializeField] protected int reserveAmmo = 45;
    [SerializeField] public AmmoType typeOfAmmo;
    [SerializeField] protected float fireRate;
    [SerializeField, Range(0,10)] protected float reloadTime;
    protected bool canShoot = true;
    protected bool canReload = true;
    private float timeToUp = 0;
    private float timeToDown = 0;
    private float yCord;

    public void addAmmo(int AmmoToAdd)
    {
        currentAmmo += AmmoToAdd;
    }

    protected void Reload()
    {
        if (reserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            int ammoNeeded = maxAmmo - currentAmmo;

            currentAmmo += ammoNeeded;
            reserveAmmo -= ammoNeeded;

        }
        else if (reserveAmmo <= 0)
        {
            Debug.Log("No Ammo");
        }
    }

    protected void Shoot()
    {
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            // hit.transform.GetComponent<Health>().Take (damage);
        }
    }

    protected IEnumerator DelayBeforeShoot(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        Shoot();
        canShoot = true;
    }

    protected IEnumerator ReloadTime(float delay)
    {
        canReload = false;
        yield return new WaitForSeconds(delay);
        Reload();
        canReload = true;
    }


}

public enum AmmoType
{
    Pistol = 0,
    MachineGun = 1,
}
