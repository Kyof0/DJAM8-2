using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;

    public int damage = 1;
    public float attackCooldown = 1.2f;
    public bool attackReady = true;
    public int AttackDuration;
    public int IdleDuration;
    public bool isAble = true;
    public bool isAttacking = false;
    public GameObject targetGO;
    public Transform gO;
    public int speed = 5;

    public float distance;

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
        gO = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        distance = Vector2.Distance(targetGO.transform.position, gO.position);
        if (Mathf.Abs(distance) < 1.5f){
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        Debug.Log($"{Vector2.Distance(targetGO.transform.position, gO.position)}");
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
            transform.position = Vector2.MoveTowards(transform.position, targetGO.transform.position, speed * Time.deltaTime);
        }
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position , 1f, enemyLayers);
        foreach (Collider2D player in hitPlayer) {
            Health hp = player.GetComponent<Health>();
            if (hp != null)
            {
                if (attackReady) {
                    attackReady = false;
                    hp.TakeDamage(damage);
                    StartCoroutine(AttackCooldown(attackCooldown));
                }
            }
        }
        if (isAble) {
            StartCoroutine(WaitFor(AttackDuration));
            isAble = false;
        }
    }
    public IEnumerator AttackCooldown(float delay)
    {
        Debug.Log("Attacking");
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