using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isKnockedDown = false;
    public bool isPoisoned = false;
    public float knockDownDuration = 2f;
    public float poisonDuration = 5f;
    private Rigidbody2D rb;
    public float knockBackForce = 5f;
    public Attack attack;
    public GameObject vulnerablePoint;
    public Animator animator;
    public SpriteRenderer sr;
    public Health health;
    public GameObject targetGO;

    public float whichSide;
    

    public bool isKnocked() {
        return isKnockedDown;
    }
    void Start()
    {
        health = targetGO.GetComponent<Health>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        vulnerablePoint = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        whichSide = vulnerablePoint.transform.position.x - targetGO.transform.position.x;
        whichSide = Mathf.Clamp(whichSide, -1, 1);
        
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
    }
    public void TakeDamage(int damage, Vector2 knockBackDirection)
    {
        if (isKnockedDown){
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(KnockDown(knockBackDirection));
        }
    }
    private IEnumerator KnockDown(Vector2 direction)
    {
        rb.AddForce(Vector2.up * knockBackForce, ForceMode2D.Impulse);
        if (whichSide > 0)
        {
            rb.AddForce(Vector2.right * knockBackForce, ForceMode2D.Impulse);
        }
        else if (whichSide < 0) {
            rb.AddForce(Vector2.left * knockBackForce, ForceMode2D.Impulse);
        }
        animator.SetBool("isMoving", false);
        animator.SetBool("isStunned", true);
        isKnockedDown = true;
        yield return new WaitForSeconds(knockDownDuration);

        if(!isPoisoned)
        {
            isKnockedDown = false;
        }
        animator.SetBool("isMoving", true);
        animator.SetBool("isStunned", false);
        vulnerablePoint.SetActive(false);
    }
    public void ApplyPoison(int poisonDamage)
    {
        if (!isKnockedDown || isPoisoned)
            return;
        isPoisoned = true;
        StartCoroutine(PoisonEffect(poisonDamage));
    }

    private IEnumerator PoisonEffect(int poisonDamage)
    {
        sr.color = new Color(247 / 255f, 246 / 255f, 149 / 255f, 255 / 255f);
        float poisonEndTime = Time.time + poisonDuration;
        while (Time.time < poisonEndTime)
        {
            currentHealth -= poisonDamage;
            health.Heal(poisonDamage / 3);

            if (currentHealth <= 0)
            {
                Die();
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }
        isKnockedDown = false;
        isPoisoned = false;
        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
