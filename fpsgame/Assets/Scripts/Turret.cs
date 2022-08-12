using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public float rangeToTargetPlayer, timeBetweenShots = .5f;
    private float shotCounter;
    public Transform gun, firepoint;
    void Start()
    {
        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer)
        {
            gun.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.5f, 0f));
            shotCounter-=Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bullet, firepoint.position, firepoint.rotation);
                shotCounter = timeBetweenShots;
            }
        } 
    }
}
