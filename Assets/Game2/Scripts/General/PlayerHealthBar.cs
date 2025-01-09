using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image healthBar;

    public void SetMaxHealth(float health)
    {
        healthBar.fillAmount = 1f;
    }

    public void SetHealth(float currentHealth, float health)
    {
        healthBar.fillAmount = currentHealth / health;
    }
}
