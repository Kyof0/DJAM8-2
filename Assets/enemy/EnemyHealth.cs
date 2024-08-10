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
    public int PoisonDamage = 10;

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
        else
        {
            StartCoroutine(KnockDown(knockBackDirection));
        }
    }

    private IEnumerator KnockDown(Vector2 direction)
    {
        isKnockedDown = true;
        yield return new WaitForSeconds(knockDownDuration);

        if(!isPoisoned)
        {
            isKnockedDown = false;
        }

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
            currentHealth -= PoisonDamage;
            if (currentHealth <= 0)
            {
                Die();
                yield break;
            }
            yield return new WaitForSeconds(1.0f);
        }
        isKnockedDown = false;
        isPoisoned = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
