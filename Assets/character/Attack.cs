using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.0f;
    public LayerMask enemyLayers;
    public float knockBackForce = 5f;
    private float lastAttackTime;
    public int PoisonDamage = 10;
    public KeyCode poisonKey = KeyCode.E; // Key for poison attack
    public KeyCode knockDownKey = KeyCode.F; // Key for knock-down attack

    // Position offset for attack area
    public Vector2 attackOffset = new Vector2(1f, 0f);

    void Update()
    {
        if (Input.GetKeyDown(knockDownKey))
        {
            PerformKnockDownAttack();
        }
        else if (Input.GetKeyDown(poisonKey))
        {
            PerformPoisonAttack();
        }
    }

    void PerformKnockDownAttack()
    {
        Debug.Log("Knockdown attack!");
        Vector2 attackPosition = (Vector2)transform.position + attackOffset;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                Vector2 knockBackDirection = (enemy.transform.position - transform.position).normalized;
                enemyScript.TakeDamage(attackDamage, knockBackDirection * knockBackForce);
            }
        }
    }

    void PerformPoisonAttack()
    {
        Debug.Log("Poison attack!");
        Vector2 attackPosition = (Vector2)transform.position + attackOffset;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.ApplyPoison(PoisonDamage);
            }
        }
    }
}
