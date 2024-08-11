using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Animator anim;
    public SpriteRenderer SpriteRenderer;
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
    public GameObject vulnerablePoint;
    public float distance;
    public float whichSide;

    public LayerMask enemyLayers;
    public float attackRange = 1.5f;

    public enum EnemyState
    {
        Idle,
        Attacking
    }
    public EnemyState currentState;
    void Start()
    {
        currentState = EnemyState.Idle;
        enemyHealth = GetComponent<EnemyHealth>();
        gO = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        vulnerablePoint = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        distance = Vector2.Distance(targetGO.transform.position, gO.position);
        whichSide = gO.position.x - targetGO.transform.position.x;
        whichSide = Mathf.Clamp(whichSide, -1, 1);
        if (whichSide > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (whichSide < 0)
        {
            SpriteRenderer.flipX = true;
        }

        if (Mathf.Abs(distance) < 1.5f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (!enemyHealth.isKnocked())
        {
            HandleState(currentState);
        }
    }

    public void HandleState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Attacking:
                Attacking();
                break;
        }
    }

    public void Idle()
    {
        if (isAble)
        {
            anim.SetBool("isMoving", true);
            StartCoroutine(WaitFor(IdleDuration));
            isAble = false;
        }

        Vector3 newPosition = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
        new Vector2(-targetGO.transform.position.x, transform.position.y), speed * Time.deltaTime);
        newPosition.z = transform.position.z;  // Maintain the current z position
        transform.position = newPosition;
    }

    public void Attacking()
    {
        if (!isAttacking)
        {
            Vector3 newPosition = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
                new Vector2(targetGO.transform.position.x, targetGO.transform.position.y), speed * Time.deltaTime);
            newPosition.z = transform.position.z;  // Maintain the current z position
            transform.position = newPosition;
        }

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, 1f, enemyLayers);
        foreach (Collider2D player in hitPlayer)
        {
            Attack playerattack = player.GetComponent<Attack>();
            Health hp = player.GetComponent<Health>();
            if (hp != null && !playerattack.isdashing)
            {
                if (attackReady)
                {
                    int dice = Random.Range(1, 20);
                    if (dice > 13)
                    {
                        StartCoroutine(VulnerableDuration(2f));
                    }
                    AudioManager.Instance.PlaySFX("brush");
                    anim.SetTrigger("Attacking");
                    attackReady = false;
                    hp.TakeDamage(damage);
                    StartCoroutine(AttackCooldown(attackCooldown));
                }
            }
        }

        if (isAble)
        {
            StartCoroutine(WaitFor(AttackDuration));
            isAble = false;
        }
    }
    public IEnumerator VulnerableDuration(float delay)
    {
        vulnerablePoint.SetActive(true);
        yield return new WaitForSeconds(delay);
        vulnerablePoint.SetActive(false);
    }
    public IEnumerator AttackCooldown(float delay)
    {
        Debug.Log("Attacking");
        yield return new WaitForSeconds(delay);
        attackReady = true;
        
    }

    public IEnumerator WaitFor(float delay)
    {
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
