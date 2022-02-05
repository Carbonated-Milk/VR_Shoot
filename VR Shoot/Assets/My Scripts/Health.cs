using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    public float currentHealth;

    public GameObject healthBar;
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.localScale = -Vector3.up * currentHealth/health + Vector3.one - Vector3.up;
        if(currentHealth <= 0)
        {
            Defeated();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Defeated()
    {
        
        GameManager.money += 500;

        FindObjectOfType<AudioManager>().CallDefeatedBoss("BossTheme");

        Destroy(gameObject);

    }
}
