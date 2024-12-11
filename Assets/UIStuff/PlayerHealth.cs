using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public TMP_Text healthText;
    [SerializeField] private Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    void Update()
    {
        UpdateHealthUI();
        UpdateHealthBar(maxHealth, currentHealth);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthUI();
    }
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth;
    }
}
