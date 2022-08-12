using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public bool isAuto;
    public bool hasInfiniteAmmo;
    public bool isRayGun;

    public float fireRate;

    public int currentAmmo;

    [HideInInspector] public float fireCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        if (hasInfiniteAmmo)
        {
            currentAmmo = 999;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }
}
