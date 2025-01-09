using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth, health;
    public SpriteRenderer spriteRenderer;
    Color originalColor;

    void Awake()
    {
        originalColor = spriteRenderer.color;

        currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(FlashRed());
        currentHealth -= damage;
        Debug.Log("took damage:" + damage + ". remaining health:" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        float flashDuration = 0.5f;
        float time = 0f;
        
        spriteRenderer.color = Color.red;

        while (time < flashDuration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Debug.Log("object death");
        Destroy(gameObject);
    }
}
