using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class RangedEnemy : MonoBehaviour
{
    public enum bossState
    {
        Attacking,
        Healing
    }
    public bool isAble = true;
    public int healingDuration;
    public int attackDuration;

    public bossState currentState;
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public int damage = 10;
    public float moveSpeed = 2f; // Speed at which the enemy moves towards the player

    public float healRange = 5f; // Range within which the enemy will heal an ally
    public int healAmount = 20;  // Amount of health to restore to an ally
    public float healCooldown = 5f; // Cooldown time for healing

    public GameObject projectilePrefab;
    public Transform firePoint;
    public LayerMask playerLayer;
    public LayerMask allyLayer; // Layer for allies

    public Animator ani;
    private Transform player;
    private bool canAttack = true;
    private bool canHeal = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = bossState.Attacking;
    }

    private void Update()
    {
        HandleState(currentState);
        
    }
    public void HandleState(bossState state)
    {
        switch (state)
        {
            case bossState.Healing:
                Healing();
                break;

            case bossState.Attacking:
                Attacking();
                break;
        }
    }
    public void Healing()
    {
        if (isAble)
        {
            isAble = false;
            StartCoroutine(WaitFor(healingDuration));
        }
        if (IsAllyInRange() && canHeal)
        {
            HealAlly();
        }
        else if (!canHeal)
        {
            Debug.Log("cant heal");
        }
    }
    public void Attacking()
    {
        if (isAble) { 
            isAble = false;
            StartCoroutine(WaitFor(attackDuration));
        }
        if (IsPlayerInRange())
        {
            if (canAttack)
            {
                AttackPlayer();
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    public IEnumerator WaitFor(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (currentState == bossState.Attacking)
        {
            currentState = bossState.Healing;
            isAble = true;
        }
        else
        {
            currentState = bossState.Attacking;
            isAble = true;
        }
    }
    private bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    private bool IsAllyInRange()
    {
        Collider2D[] alliesInRange = Physics2D.OverlapCircleAll(transform.position, healRange, allyLayer);
        return alliesInRange.Length > 0;
    }

    private void HealAlly()
    {

        Collider2D[] alliesInRange = Physics2D.OverlapCircleAll(transform.position, healRange, allyLayer);
        foreach (Collider2D ally in alliesInRange)
        {
            EnemyHealth allyHealth = ally.GetComponent<EnemyHealth>();
            if (allyHealth != null && allyHealth.currentHealth < allyHealth.maxHealth)
            {
                Debug.Log("heal complete");
                allyHealth.currentHealth += healAmount;
                allyHealth.currentHealth = Mathf.Clamp(allyHealth.currentHealth, 0, allyHealth.maxHealth);
                StartCoroutine(HealCooldown(healCooldown)); // Start cooldown after healing
                canHeal = false; // Prevent healing until cooldown is over
                break; // Only heal one ally at a time
            }
        }
    }

    private void AttackPlayer()
    {
        canAttack = false;
        ShootProjectile();
        StartCoroutine(AttackCooldown(attackCooldown));
    }

    private void ShootProjectile()
    {
        ani.SetTrigger("attack");
        StartCoroutine(animationtilshoot());
    }

    private IEnumerator animationtilshoot()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDamage(damage);
            projScript.SetTarget(player);
        }
    }

    private IEnumerator AttackCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }

    private IEnumerator HealCooldown(float delay)
    {
        yield return new WaitForSeconds(healCooldown);
        canHeal = true;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
