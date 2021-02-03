using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Rigidbody2D rb;

    public GameObject bulletImpact;

    public Camera viewCam;

    public Text healthText, ammoText, killText;

    public Animator gunAnim,moveAnim;

    public int ammo, killCount;
    public int currentHealth;
    public int maxHealth = 100;

    public float speed = 5f;
    public float mouseSense = 2f;

    private Vector2 moveInput, mouseInput;
    private bool isDead;

    


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
        ammoText.text = ammo.ToString();
        killText.text ="Kills: "+killCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            //player movement
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            Vector3 moveH = transform.up * -moveInput.x;
            Vector3 moveV = transform.right * moveInput.y;

            rb.velocity = (moveH + moveV) * speed;

            //player view control
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSense;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

            viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

            //player shooting

            if (Input.GetMouseButtonDown(0))
            {
                if (!AudioController.instance.playerShot.isPlaying)
                {
                    AudioController.instance.playerShot.Play();
                }
                if (ammo > 0)
                {
                    Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Instantiate(bulletImpact, hit.point, transform.rotation);
                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                        }
                        if (hit.transform.tag == "Boss")
                        {
                            hit.transform.parent.GetComponent<Boss>().TakeDamage();
                        }
                    }
                    else
                    {

                    }
                    ammo--;
                    ammo = Mathf.Clamp(ammo, 0, 1000);
                    gunAnim.SetTrigger("Shoot");
                    UpdateAmmoUI();
                }
            }
        }
        if (moveInput != Vector2.zero)
        {
            moveAnim.SetBool("IsMoving", true);
            if (!AudioController.instance.playerFootsteps.isPlaying)
            {
                AudioController.instance.playerFootsteps.Play();
            }
        }
        else
        {
            moveAnim.SetBool("IsMoving", false);
        }
    }
    public void AddHealth(int HealAmount)
    {
        currentHealth += HealAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthText.text = currentHealth.ToString() + "%";
    }
    public void TakeDamage(int DamageAmount)
    {
        currentHealth -= DamageAmount;

        if (currentHealth <= 0)
        {
            if (!AudioController.instance.playerDeath.isPlaying)
            {
                AudioController.instance.playerDeath.Play();
            }
            isDead = true;
            currentHealth = 0;
        }
       healthText.text = currentHealth.ToString() + "%";
    }
    public void UpdateAmmoUI()
    {
        ammoText.text = ammo.ToString();
    }
    public void UpdateKillsUI()
    {
        killCount++;
        killText.text = "Kills: " + killCount.ToString();
    }
}
