using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public SpriteRenderer sr;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(goRed(2f));
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public IEnumerator goRed(float delay) { 
        sr.color = Color.red;
        yield return new WaitForSeconds(delay);
        sr.color = Color.white;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void Die()
    {
        Debug.Log("Main character has died.");
        SceneManager.LoadScene("entrance");
    }
}
