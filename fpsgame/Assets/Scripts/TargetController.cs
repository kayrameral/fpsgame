using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public GameObject nextTarget;
    public int targetID;

    // Start is called before the first frame update
    void Start()
    {
        if (targetID > 0)
        {
            gameObject.SetActive(false);
        }

        UIController.instance.targetText.text = "0/" + GameManager.instance.numberOfTargets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageTarget()
    {
        if (nextTarget != null)
        {
            nextTarget.SetActive(true); 
        }
        int targetState = ++GameManager.instance.targetInfo;
        UIController.instance.targetText.text = targetState + "/" + GameManager.instance.numberOfTargets;
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(7);
    }
}
