using UnityEngine;
using System.Collections;
public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isKnockedDown = false;
    private bool isPoisoned = false;
    public float knockDownDuration = 2f;
    public float poisonDuration = 5f;
    private Rigidbody2D rb;
    public float knockBackForce = 5f;
    public VulnerablePoint vulnerablePoint;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 knockBackDirection)
    {
        if (isKnockedDown)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (vulnerablePoint.IsVisible())
        {
            StartCoroutine(KnockDown(knockBackDirection));
        }
    }

    private IEnumerator KnockDown(Vector2 direction)
    {
        isKnockedDown = true;
        rb.AddForce(direction * knockBackForce, ForceMode2D.Impulse);


        yield return new WaitForSeconds(knockDownDuration);

        isKnockedDown = false;

    }

    public void ApplyPoison()
    {
        if (!isKnockedDown || isPoisoned)
            return;

        isPoisoned = true;

        StartCoroutine(PoisonEffect());
    }

    private IEnumerator PoisonEffect()
    {
        float poisonEndTime = Time.time + poisonDuration;

        while (Time.time < poisonEndTime)
        {
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                Die();
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }

        isPoisoned = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
