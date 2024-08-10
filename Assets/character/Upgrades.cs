using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int coins;

    public Health health;
    public Attack attack;

    public int healthUpgradeCost = 10;
    public int damageUpgradeCost = 15;
    public int cooldownUpgradeCost = 20;

    public float costMultiplier = 1.5f; // Cost increases by 50% after each upgrade

    public void UpgradeHealth()
    {
        if (coins >= healthUpgradeCost)
        {
            coins -= healthUpgradeCost;
            health.maxHealth += 10;
            Debug.Log("Health upgraded!");

            // Increase the cost for the next upgrade
            healthUpgradeCost = Mathf.CeilToInt(healthUpgradeCost * costMultiplier);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade health.");
        }
    }

    public void UpgradeDamage()
    {
        if (coins >= damageUpgradeCost)
        {
            coins -= damageUpgradeCost;
            attack.attackDamage += 2;
            Debug.Log("Damage upgraded!");

            // Increase the cost for the next upgrade
            damageUpgradeCost = Mathf.CeilToInt(damageUpgradeCost * costMultiplier);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade damage.");
        }
    }

    public void UpgradeCooldown()
    {
        if (coins >= cooldownUpgradeCost)
        {
            coins -= cooldownUpgradeCost;
            attack.attackCooldown -= 0.1f;
            Debug.Log("Cooldown upgraded!");

            // Increase the cost for the next upgrade
            cooldownUpgradeCost = Mathf.CeilToInt(cooldownUpgradeCost * costMultiplier);
        }
        else
        {
            Debug.Log("Not enough coins to upgrade cooldown.");
        }
    }
}
