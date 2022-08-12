using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBarrierButton : MonoBehaviour
{
    public int barrierHealth;
    public GameObject barrier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitOnButton()
    {
        barrierHealth--;

        if(barrierHealth <= 0)
        {
            Destroy(barrier);
        }
    }
}
