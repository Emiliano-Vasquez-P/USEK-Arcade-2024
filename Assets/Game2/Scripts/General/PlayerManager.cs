using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float currentHealth, health;

    [SerializeField] 
    float invincibilityDuration = 3f;
    public bool isInvincible = false;

    void Awake()
    {
        currentHealth = health;

        PlayerHealthBar healthBar = FindObjectOfType<PlayerHealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
            healthBar.SetHealth(currentHealth, health);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Took damage: {damage}. Remaining health: {currentHealth}");

        PlayerHealthBar healthBar = FindObjectOfType<PlayerHealthBar>();
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, health);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(float amount)
    {
        if (amount < 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, health);
        Debug.Log($"Restored health: {amount}. Current health: {currentHealth}");
    }

    public void BecomeInvincible(bool value)
    {
        if(value == true)
        {
            StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    IEnumerator BecomeTemporarilyInvincible()
    {
        if (!isInvincible)
        {
            Debug.Log("Player turned invincible");
            isInvincible = true;

            yield return new WaitForSeconds(invincibilityDuration);

            isInvincible = false;
            Debug.Log("Player no longer invincible");
        }
    }

    private void Die()
    {
        Debug.Log("Object death");
        GameSceneManager.GameOver();
        Destroy(gameObject);
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
