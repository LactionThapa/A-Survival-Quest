using System;
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
    [SerializeField, Range(0, 10)] protected float reloadTime;
    protected bool canShoot = true;
    protected bool canReload = true;
    private Ray ray;
    [SerializeField] public GameObject ShootPoint;
    [SerializeField] public GameObject RayStartPoint;
    [SerializeField] private LayerMask EnemyLayer;
    public static Action<int, int> showAmmo;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip addAmmoClip;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private GameObject ShotSmokePosition;
    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private TrailRenderer bulletTracer;
    private float time;
    public void addAmmo(int AmmoToAdd)
    {
        reserveAmmo += AmmoToAdd;
        showAmmo.Invoke(currentAmmo, reserveAmmo);
        audioSource.clip = addAmmoClip;
        audioSource.Play();
    }

    protected void Reload()
    {
        if (reserveAmmo > 0 && currentAmmo < maxAmmo)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            currentAmmo += ammoNeeded;
            reserveAmmo -= ammoNeeded;
            showAmmo.Invoke(currentAmmo, reserveAmmo);

        }
        else if (reserveAmmo <= 0)
        {
            Debug.Log("No Ammo");
            showAmmo.Invoke(currentAmmo, reserveAmmo);
        }
    }

    void Start()
    {
        Debug.Log(RayStartPoint);
        currentAmmo = maxAmmo;
        showAmmo.Invoke(currentAmmo, reserveAmmo);
    }

    public void Update()
    {

        ray = new Ray(RayStartPoint.transform.position, SetDirectionToShoot());
        time += Time.deltaTime / bulletTracer.time;

        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0)
            {
                if (canShoot)
                {
                    StartCoroutine(DelayBeforeShoot(fireRate));
                    audioSource.clip = fireClip;
                    audioSource.Play();
                }
            }
            else
            {
                Debug.Log("No Ammo");
                showAmmo.Invoke(currentAmmo, reserveAmmo);
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
        return direction;
    }
    protected void Shoot()
    {
        currentAmmo--;
        showAmmo.Invoke(currentAmmo, reserveAmmo);
        Debug.DrawRay(RayStartPoint.transform.position, SetDirectionToShoot() * 100, Color.red);
        var rotation = ShotSmokePosition.transform.rotation;
        var particle = Instantiate(shootParticle, ShotSmokePosition.transform.position, rotation);
        StartCoroutine(WaitBeforeVanish(0.2f, particle.gameObject));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, EnemyLayer))
        {
            Debug.Log(hit.transform.name);
            hit.transform.TryGetComponent(out ZombieStats stats);
            stats?.TakeDamage(Damage);
            var trail = Instantiate(bulletTracer, ShotSmokePosition.transform.position, Quaternion.identity);
            if (stats != null)
            {
                StartCoroutine(TrailCalculation(ShotSmokePosition.transform.position, stats.bloodSprayPosition.transform.position, trail.gameObject));
            }
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

    private void OnEnable()
    {
        showAmmo.Invoke(currentAmmo, reserveAmmo);
    }
    private IEnumerator WaitBeforeVanish(float duration, GameObject particle)
    {
        yield return new WaitForSeconds(duration);
        Destroy(particle);
    }
    private void BulletTracing(Vector3 startPoint, Vector3 endPoint, GameObject trail)
    {
        var positionX = Mathf.Lerp(startPoint.x, endPoint.x, time);
        var positionY = Mathf.Lerp(startPoint.y, endPoint.y, time);
        var positionZ = Mathf.Lerp(startPoint.z, endPoint.z, time);
        trail.transform.position = new Vector3(positionX, positionY, positionZ);
    }
    private IEnumerator TrailCalculation(Vector3 startPoint, Vector3 endPoint, GameObject trail)
    {
        time = 0;
        while (time < 1)
        {
            Debug.Log(time);
            BulletTracing(startPoint, endPoint, trail);
            yield return null;
        }
    }


}

public enum AmmoType
{
    Pistol = 0,
    MachineGun = 1,
}
