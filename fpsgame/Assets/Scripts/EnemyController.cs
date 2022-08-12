using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool isAlive = true;
    public Transform target;
    private bool chasing;
    public float distanceToChase = 12f;
    public float distanceToLose = 15f;
    public float distanceToStop = 10f;
    public float distanceToStopRunning = 2f;


    private Vector3 targetPoint;
    private Vector3 startPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 15f;
    public float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    public float fireRate;
    private float fireCount;
    public float holdFire = 2f;
    public float openFire = 1f;
    private float holdFireCounter;
    private float openFireCounter;

    public Animator anim;

    public GameObject blaster;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        startPoint = transform.position;
        holdFireCounter = holdFire;
        openFireCounter = openFire;
        blaster.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;
        if (isAlive && PlayerController.instance.gameObject.activeInHierarchy) {
            if (!chasing)
            {
                anim.SetBool("isFiring", false);

                if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
                {
                    chasing = true;
                    //fireCount = 1f;
                    holdFireCounter = holdFire;
                    openFireCounter = openFire;
                }

                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }

                if (Vector3.Distance(transform.position, startPoint) <= distanceToStopRunning)
                {
                    anim.SetBool("isMoving", false);
                    blaster.SetActive(false);
                }

            }
            else
            {
                blaster.SetActive(true);

                anim.SetBool("isMoving", true);
                anim.SetBool("isFiring", true);

                // transform.LookAt(targetPoint);

                //  theRB.velocity = transform.forward * moveSpeed;

                if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {
                    anim.SetBool("isMoving", true);

                    agent.destination = targetPoint;

                }
                else
                {
                    anim.SetBool("isMoving", false);

                    agent.destination = transform.position;
                    transform.LookAt(targetPoint);

                }

                if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
                {
                    chasing = false;

                    chaseCounter = keepChasingTime;
                }

                if (holdFireCounter > 0)
                {
                    holdFireCounter -= Time.deltaTime;

                    if (holdFireCounter <= 0)
                    {
                        openFireCounter = openFire;
                    }

                }
                else
                {
                    openFireCounter -= Time.deltaTime;


                    if (openFireCounter > 0)
                    {
                        fireCount -= Time.deltaTime;

                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.instance.transform.position);

                            Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {
                                Instantiate(bullet, firePoint.position, firePoint.rotation);

                            }
                            else
                            {
                                holdFireCounter = holdFire;
                            }
                        }
                    }
                    else
                    {
                        holdFireCounter = holdFire;
                    }
                }
            }
        }
        else
        {
            if (isAlive && blaster.activeInHierarchy)
            {
                anim.SetTrigger("enemyWon");
            }
            blaster.SetActive(false);
        }
    }
}
