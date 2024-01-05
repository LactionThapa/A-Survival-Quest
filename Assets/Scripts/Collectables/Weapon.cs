using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected int currentAmmo;
    [SerializeField] public int Damage;
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
    private Vector3 directionToShoot;
    private Ray ray;
    [SerializeField] public GameObject ShootPoint;
    [SerializeField] public GameObject RayStartPoint;
    [SerializeField] private LayerMask EnemyLayer;

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

    void Start()
    {
        Debug.Log(RayStartPoint);
        currentAmmo = maxAmmo;
    }

    public void Update()
    {

        ray = new Ray(RayStartPoint.transform.position, SetDirectionToShoot());

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

    public Vector3 SetDirectionToShoot()
    {
        Vector3 direction = (ShootPoint.transform.position - RayStartPoint.transform.position).normalized;
        //directionToShoot = (ShootPoint - transform.TransformPoint(transform.position)).normalized;
        //Debug.Log(direction);
        //Debug.Log(directionToShoot);
        return direction;
    }
    protected void Shoot()
    {
        currentAmmo--;
        Debug.DrawRay(RayStartPoint.transform.position, SetDirectionToShoot() * 100, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, EnemyLayer))
        {
            Debug.Log(hit.transform.name);
            hit.transform.TryGetComponent(out ZombieStats stats);
            stats?.TakeDamage(Damage);


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
