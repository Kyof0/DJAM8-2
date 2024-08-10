using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public int damage = 10;
    public float moveSpeed = 2f; // Speed at which the enemy moves towards the player

    public GameObject projectilePrefab;
    public Transform firePoint;
    public LayerMask playerLayer;

    private Transform player;
    private bool canAttack = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
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
    }

    private bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    private void AttackPlayer()
    {
        canAttack = false;
        ShootProjectile();
        StartCoroutine(AttackCooldown(attackCooldown));
    }

    private void ShootProjectile()
    {
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

    private void MoveTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector2 direction = (player.position - transform.position).normalized;
        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
