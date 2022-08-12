using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHealth = 3;
    public Animator anim;
    public EnemyController enemyController;
    private float deceaseCounter = 5f;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { 
            deceaseCounter -= Time.deltaTime;
            if (deceaseCounter < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void DamageEnemy(int damage)
    {
        currentHealth -= damage;
        
        if(currentHealth <= 0 && enemyController.isAlive == true)
        {
            enemyController.isAlive = false;
            anim.SetTrigger("dying");
            isDead = true;
            GameManager.instance.enemyInfo++;
        }
    }
    public void DamageEnemyTurret(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
