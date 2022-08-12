using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMine : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserBeam;

    public bool isTriggered;

    public float fireDelay;
    public float fireCount;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
        fireCount = fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            fireCount -= Time.deltaTime;
            
            if(fireCount <= 0)
            {
                Instantiate(laserBeam, firePoint.position, firePoint.rotation);
                fireCount = fireDelay;
                isTriggered = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            isTriggered=true;
            AudioManager.instance.PlaySFX(2);
        }
    }
}
