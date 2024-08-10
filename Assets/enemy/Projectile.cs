using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private int damage;
    private Vector3 targetPosition;

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    public void SetTarget(Transform targetTransform)
    {
        // Capture the player's position at the time of shooting
        targetPosition = targetTransform.position;
    }

    void Update()
    {
        // Move the projectile towards the stored target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the projectile has reached the target position
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        // Check if the target is still there and deal damage
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D target in hitTargets)
        {
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }

        // Destroy the projectile after hitting the target
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a small circle at the impact point
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
