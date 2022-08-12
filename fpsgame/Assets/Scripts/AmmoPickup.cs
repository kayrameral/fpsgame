using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int blasterID;
    public int ammoAmount;
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
        if (other.tag == "Player")
        {
            PlayerController.instance.AddAmmo(ammoAmount, blasterID);
            AudioManager.instance.PlaySFX(3);
        }

        Destroy(gameObject);
    }
}
