using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    // Start is called before the first frame update
    public float bounceForce=20f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.Bounce(bounceForce);
            AudioManager.instance.PlaySFX(0);
        }
    }
}