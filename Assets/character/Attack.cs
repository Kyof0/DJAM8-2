using System.Collections;
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
    private Vector2 dashDirection;
    public GameObject vulnerablePoint;
    public float Stamina = 20f;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public Sprite mainSprite;
    public bool isdashing;
    // Dash variables
    public float dashDistance = 5f;
    public float dashSpeed = 10f;
    public float dashCooldown = 2f;
    public bool canDash = true;
    public KeyCode dashKey = KeyCode.Space; // Key for dashing
    public barManager barManager;
    public GameObject barManagerGO;
    // Position offset for attack area
    public Vector2 attackOffset = new Vector2(1f, 0f);

    void Start()
    {
        barManager = barManagerGO.GetComponent<barManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

        if (Input.GetKeyDown(dashKey) && canDash && Stamina > 5)
        {
            StartCoroutine(Dash());
        }

        Stamina += 0.01f;
        Stamina = Mathf.Clamp(Stamina, 0, 20f);
        GainStamina(0.01f);
    }
    public void GainStamina(float amount)
    {
        barManager.GainStamina(amount);
    }
    public void SpendStamina(float cost)
    {
        barManager.SpendStamina(cost);
    }
    public IEnumerator changeSprite(float delay)
    {
        spriteRenderer.sprite = sprite;
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = mainSprite;
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
                if (enemy.transform.GetChild(0).gameObject.activeSelf)
                {
                    if (Stamina > 5f)
                    {
                        SpendStamina(5f);
                        Stamina -= 5f;
                        AudioManager.Instance.PlaySFX("tooth");
                        StartCoroutine(changeSprite(0.5f));
                        Vector2 knockBackDirection = (enemy.transform.position - transform.position).normalized;
                        enemyScript.TakeDamage(attackDamage, knockBackDirection * knockBackForce);
                        enemy.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
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
                if (Stamina > 5f)
                {
                    SpendStamina(5f);
                    Stamina -= 5f;
                    enemyScript.ApplyPoison(PoisonDamage);
                }
            }
        }
    }

    private IEnumerator Dash()
    {
        SpendStamina(5f);
        Stamina -= 5f;
        isdashing = true;
        canDash = false;
        Color color = spriteRenderer.color;
        color.a = 0.5f; // 50% transparent
        spriteRenderer.color = color;

        float dashTime = dashDistance / dashSpeed;
        if(transform.localScale.x == 1)
        {
           dashDirection = transform.right; // Dash in the direction the player is facing
        }
        else
        {
            dashDirection = transform.right * -1;
        }

        // Perform the dash
        for (float t = 0; t < dashTime; t += Time.deltaTime)
        {
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        // Reset transparency
        color.a = 1f; // Fully opaque
        spriteRenderer.color = color;
        isdashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
