using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    protected int health;
    private int damage;
    public float attackSpeed;
    protected bool isDead;
    private bool canAttack;

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
        health -= incomingDamage;
        CheckHealth();
    }
    public int DealDamage(int statsToDamage)
    {
        statsToDamage -= damage;
        return statsToDamage;
    }
    void Update()
    {

    }
}
