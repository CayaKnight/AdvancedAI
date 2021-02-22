using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;

    public Camera viewCam;

    public float moveSpeed = 0;
    public float mouseSense = 1;

    //public Text healthText, ammoText, killText;

    //public Animator gunAnim;
    //public Animator moveAnim;

    //public GameObject bulletImpact;
    ////public GameObject gameOver;

    //public int ammo;
    //public int killCount = 0;
    //public int currentHealth;
    //public int maxHealth = 100;



    //private bool isDead;
    private Vector2 moveInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        //healthText.text = currentHealth.ToString() + "%";
        //ammoText.text = ammo.ToString();
        //killText.text = "Kills: "+killCount.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveH = transform.up * -moveInput.x;
        Vector3 moveV = transform.right * moveInput.y;



        rb.velocity = (moveH + moveV) * moveSpeed;

        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSense;


        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
        //Mathf.Clamp(mouseInput.y, -45f, 45f);

        viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
        //if (!isDead)
        //{
        //moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //Vector3 moveH = transform.up * -moveInput.x;
        //Vector3 moveV = transform.right * moveInput.y;



        //rb.velocity = (moveH + moveV) * moveSpeed;

        //mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSense;


        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);
        ////Mathf.Clamp(mouseInput.y, -45f, 45f);

        //viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        if (ammo > 0)
        //        {
        //            Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));

        //            RaycastHit hit;

        //            if (Physics.Raycast(ray, out hit))
        //            {
        //                //Debug.Log("Looking at "+ hit.transform.name);
        //                Instantiate(bulletImpact, hit.point, transform.rotation);
        //                AudioController.instance.PlayPlayerShot();

        //                if (hit.transform.tag == "Enemy")
        //                {
        //                    hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
        //                }

        //                //if (hit.transform.tag == "Boss")
        //                //{
        //                //    hit.transform.parent.GetComponent<Boss>().TakeDamage();

        //                //}
        //            }

        //        }
        //        ammo--;
        //        ammo = Mathf.Clamp(ammo, 0, 1000);
        //        gunAnim.SetTrigger("Shoot");
        //        UpdateAmmoUI();
        //    }
        //}

        //    if (moveInput != Vector2.zero)
        //    {
        //        moveAnim.SetBool("IsMoving", true);
        //        AudioController.instance.PlayPlayerMove();
        //    }
        //    else
        //    {
        //        moveAnim.SetBool("IsMoving", false);
        //    }

    }
    //public void TakeDamage(int DamageAmount)
    //{
    //    AudioController.instance.PlayPlayerHurt();
    //    currentHealth -= DamageAmount;

    //    if (currentHealth <= 0)
    //    {

    //        isDead = true;
    //        currentHealth = 0;

    //    }
    //    healthText.text = currentHealth.ToString() + "%";
    //}

    //public void AddHealth(int HealAmount)
    //{
    //    currentHealth += HealAmount;

    //    if (currentHealth > maxHealth)
    //    {
    //        currentHealth = maxHealth;
    //    }
    //    healthText.text = currentHealth.ToString() + "%";
    //}

    //public void UpdateAmmoUI()
    //{
    //    ammoText.text = ammo.ToString();
    //}

    //public void UpdateKillsUI()
    //{
    //    killCount++;
    //    killText.text = killCount.ToString();
    //}
}
