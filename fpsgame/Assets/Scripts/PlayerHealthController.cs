using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + "%";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + "%";

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(6);
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + "%";
    }
}
