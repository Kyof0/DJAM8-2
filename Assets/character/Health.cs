using System.Collections;
using System.Collections.Generic;
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
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        StartCoroutine(goRed(0.6f));
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
