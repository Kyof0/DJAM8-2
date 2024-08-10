using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.0f;
    public LayerMask enemyLayers;
    public float knockBackForce = 5f;
    private float lastAttackTime;

    public KeyCode poisonKey = KeyCode.E; // Key for poison attack
    public KeyCode knockDownKey = KeyCode.F; // Key for knock-down attack

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
        Debug.Log("knockdown");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, 0.5f, enemyLayers);

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
        Debug.Log("posion");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + transform.right * attackRange, 0.5f, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.ApplyPoison();
            }
        }
    }
}
