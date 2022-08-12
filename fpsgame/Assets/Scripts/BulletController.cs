using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed,lifeTime;
    public Rigidbody theRB;
    public int damage = 1;
    public bool damageEnemy;
    public bool damagePlayer;

    public GameObject impactEffect;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
            //Destroy(other.gameObject);
        }else
        if (other.gameObject.tag == "Head" && damageEnemy)
        {
            other.transform.parent.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 3);
            //Destroy(other.gameObject);
        }
        else
        if (other.gameObject.tag == "Turret" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemyTurret(damage);
            //Destroy(other.gameObject);
        }else
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            PlayerHealthController.instance.DamagePlayer(damage);

        }else
        if (other.gameObject.tag == "Target" && damageEnemy)
        {
            other.gameObject.GetComponent<TargetController>().DamageTarget();
        }else
        if (other.gameObject.tag == "BarrierButton" && damageEnemy)
        {
            other.gameObject.GetComponent<LaserBarrierButton>().HitOnButton();
        }
		
            


        Destroy(gameObject);
        Instantiate(impactEffect, transform.position +(transform.forward*(-moveSpeed*Time.deltaTime)), transform.rotation);
    }
}
