using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed, gravityModifier, jumpPower, runSpeed = 18f;
    public CharacterController charCon;

    private Vector3 moveInput;

    public Transform camTrans;

    public float mouseSensivity;
    public bool invertX;
    public bool invertY;

    private float bounceAmount;
    private bool bounce;
    private bool canJump,canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    public Animator anim;
    public GameObject bullet;
    public GameObject laserBeam;
    public Transform firePoint;

    public Blaster activeBlaster;
    public List<Blaster> inventory = new List<Blaster>();
    public List<Blaster> unlockable = new List<Blaster>();
    public int activeBlasterID;

    public bool cheatsActive;

    private void Awake()
    {
        instance = this;
    } 

void Start()
    {
        activeBlaster = inventory[activeBlasterID];
        activeBlaster.gameObject.SetActive(true);
        UIController.instance.ammoText.text = activeBlaster.currentAmmo + "";
    }

void Update()
{
    
    if (!UIController.instance.pauseScreen.activeInHierarchy)
    {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //store y velocity
        float yStore = moveInput.y;


        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);
       

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }
        else
        {
            moveInput = moveInput * moveSpeed;
        }

        moveInput.y = yStore;
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, 3f, whatIsGround).Length > 0;

        
        //Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlaySFX(5);
            if (canJump)
            {
                moveInput.y = jumpPower;

                canDoubleJump = true;
				
            }
            else if (canDoubleJump == true)
            {

                moveInput.y = jumpPower;

                canDoubleJump = false;
				
            }
        }
        if (bounce)
        {
            bounce = false;
            moveInput.y = bounceAmount;
            canDoubleJump = true;
        }
        if (transform.position.y<-12 && transform.position.y>-22)
        {
            PlayerHealthController.instance.DamagePlayer(100);
        }
        else
        charCon.Move(moveInput * Time.deltaTime);


        //control camera rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }

        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        //Handle Shooting
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            if (activeBlaster.fireCounter <= 0)
            {
                if (activeBlaster.isRayGun)
                {
                    FireLaserBeam();
                }
                else
                {
                    FireShot();
                }
            }
        }
        

        if (Input.GetMouseButton(0) && activeBlaster.isAuto)
        {
            if (activeBlaster.fireCounter <= 0)
            {
                FireShot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activeBlaster.gameObject.SetActive(false);
            activeBlasterID = ++activeBlasterID % inventory.Count;

            activeBlaster = inventory[activeBlasterID];
            activeBlaster.gameObject.SetActive(true);
            UIController.instance.ammoText.text = activeBlaster.currentAmmo + "";
        }

        if (Input.GetKeyDown(KeyCode.O) && cheatsActive)
        {
            AddAmmo(20, activeBlasterID);
        }

        if (Input.GetKeyDown(KeyCode.L) && cheatsActive)
        {
            PlayerHealthController.instance.maxHealth += 100;
            PlayerHealthController.instance.HealPlayer(100);
        }

        if (Input.GetKeyDown(KeyCode.M) && cheatsActive)
        {
                if (runSpeed < 40f)
                {
                    runSpeed = 40f;
                }
                else
                {
                    runSpeed = 18f;
                }
        }

        anim.SetFloat("moveSpeed", moveInput.magnitude);
        anim.SetBool("onGround", canJump);
    }
}

public void FireShot()
    {
        if (activeBlaster.hasInfiniteAmmo || activeBlaster.currentAmmo > 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);

            activeBlaster.fireCounter = activeBlaster.fireRate;
            if (!activeBlaster.hasInfiniteAmmo)
            {
                activeBlaster.currentAmmo--;
                UIController.instance.ammoText.text = activeBlaster.currentAmmo + "";
            }
        }
    }

    public void FireLaserBeam()
    {
        if (activeBlaster.currentAmmo > 0)
        {
            Instantiate(laserBeam, firePoint.position, firePoint.rotation);
            activeBlaster.currentAmmo--;
            UIController.instance.ammoText.text = activeBlaster.currentAmmo + "";
            activeBlaster.fireCounter = activeBlaster.fireRate;
        }
    }

    public void AddAmmo(int ammoAmount, int blasterID)
    {
        inventory[blasterID].currentAmmo += ammoAmount;
        UIController.instance.ammoText.text = activeBlaster.currentAmmo + "";
    }

    public void UnlockWeapon(int unlockableID)
    {
        inventory.Add(unlockable[unlockableID]);
    }
    public void Bounce(float bounceForce)
    {
        bounceAmount = bounceForce;
        bounce = true;
    }
    
    
}
