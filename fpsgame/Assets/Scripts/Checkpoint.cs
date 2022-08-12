using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public string cpName;
    void Start()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == cpName)
            {
                PlayerController.instance.transform.position = transform.position;
               
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name+ "_cp", cpName);
			AudioManager.instance.PlaySFX(1);
        }
    }
}
