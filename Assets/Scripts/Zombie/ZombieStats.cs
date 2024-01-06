using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    protected int health;
    public int damage { get; private set; }
    public float attackSpeed;
    protected bool isDead;
    private bool canAttack;
    [field: SerializeField] public PlayerController playerController { get; private set; }
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] public GameObject bloodSprayPosition;

    void Start()
    {
        health = 50;
        isDead = false;
        damage = 10;
        attackSpeed = 1.5f;
        canAttack = true;
    }
    public void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Dead();
        }
    }
    public void Dead()
    {
        isDead = true;
        Destroy(gameObject);
    }
    public void TakeDamage(int incomingDamage)
    {
        if (health - incomingDamage > 0 && isDead == false)
        {
            health -= incomingDamage;
        var blood = Instantiate(bloodParticle, bloodSprayPosition.transform.position, Quaternion.identity);
        StartCoroutine(WaitBeforeVanish(0.2f, blood.gameObject));
        }
        else if (isDead == false)
        {
            health = 0;
            isDead = true;
            var blood = Instantiate(bloodParticle, bloodSprayPosition.transform.position, Quaternion.identity);
            StartCoroutine(WaitBeforeVanish(0.2f, blood.gameObject));
            StartCoroutine(WaitBeforeVanish(0.21f, gameObject));
        }
    }
    public int DealDamage(int statsToDamage)
    {
        statsToDamage -= damage;
        return statsToDamage;
    }

    private IEnumerator WaitBeforeVanish(float duration, GameObject particle)
    {
        yield return new WaitForSeconds(duration);
        Destroy(particle);
    }
}
