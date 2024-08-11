using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    public Image energyBar;
    public float energyAmount = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount/100f;
    }
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount,0,100);
        healthBar.fillAmount = healthAmount / 100f;
    }
    public void SpendStamina(float cost)
    {
        energyAmount -= cost;
        energyAmount = Mathf.Clamp(energyAmount, 0, 20);
        energyBar.fillAmount = healthAmount / 20f;
    }
    public void GainStamina(float gainingAmount)
    {
        energyAmount += gainingAmount;
        energyAmount = Mathf.Clamp(energyAmount, 0, 20);
        energyBar.fillAmount = energyAmount / 20f;
    }
}
