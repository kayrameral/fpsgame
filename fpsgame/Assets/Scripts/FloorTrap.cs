using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserBeam;

    public float fireRate;
    public float fireCount;

    // Start is called before the first frame update
    void Start()
    {
        fireCount = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireCount -= Time.deltaTime;
        if(fireCount <= 0)
        {
            Instantiate(laserBeam, firePoint.position, firePoint.rotation);
            fireCount = fireRate;
        }
    }
}
