using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    public int damage = 5;
    public float attackCooldown = 1.2f;
    public bool attackReady = true;
    public int AttackDuration;
    public int IdleDuration;
    public bool isAble = true;
    public bool isAttacking = false;
    public GameObject target;
    public int speed = 5;

    public LayerMask enemyLayers;
    public float attackRange = 1.5f;

    public enum EnemyState {
        Idle,
        Attacking
    }
    public EnemyState currentState;
    void Start() {
        currentState = EnemyState.Idle;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update() {
        if (!enemyHealth.isKnocked()) {
            HandleState(currentState);
        }
    }
    public void HandleState(EnemyState state) {
        switch (state) {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }
    public void Idle() {
        if (isAble) {
            StartCoroutine(WaitFor(IdleDuration));
            isAble = false;
        }
    }
    public void Attacking() {
        if (!isAttacking) {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, 0.5f, enemyLayers);
        foreach (Collider2D player in hitPlayer) {
            Health hp = player.GetComponent<Health>();
            if (hp != null)
            {
                isAttacking = true;
                if (attackReady) {
                    attackReady = false;
                    hp.TakeDamage(damage);
                }
                StartCoroutine(AttackCooldown(attackCooldown));
            }
            else {
                isAttacking = false;
            }
        }
        if (isAble) {
            StartCoroutine(WaitFor(AttackDuration));
            isAble = false;
        }
    }
    public IEnumerator AttackCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        attackReady = true;
    }
    public IEnumerator WaitFor(float delay){
        Debug.Log($"{currentState}: {delay}");
        yield return new WaitForSeconds(delay);
        if (currentState == EnemyState.Idle)
        {
            currentState = EnemyState.Attacking;
            isAble = true;
        }
        else
        {
            currentState = EnemyState.Idle;
            isAble = true;
        }
    }
}
