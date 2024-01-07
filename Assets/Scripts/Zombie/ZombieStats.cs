using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] public int damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] public GameObject bloodSprayPosition;
    [field: SerializeField] public PlayerController playerController { get; private set; }
    private Animator animator = null;
    protected bool isDead;
    private bool canAttack;


    void Start()
    {
        isDead = false;
        canAttack = true;
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("Damage");
            var blood = Instantiate(bloodParticle, bloodSprayPosition.transform.position, Quaternion.identity);
        StartCoroutine(WaitBeforeVanish(0.2f, blood.gameObject));
        }
        else if (isDead == false)
        {
            health = 0;
            isDead = true;
            animator.SetTrigger("Damage");
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
    public bool IsDead()
    {
        return isDead;
    }
}
