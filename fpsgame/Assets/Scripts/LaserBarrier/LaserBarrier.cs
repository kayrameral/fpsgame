using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarrier : MonoBehaviour
{
    public int damage;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damage);

        }
        Instantiate(impactEffect, transform.position + (transform.forward * (-20 * Time.deltaTime)), transform.rotation);
    }
}
