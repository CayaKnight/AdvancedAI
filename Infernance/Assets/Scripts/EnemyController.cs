using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public GameObject explosion;
    public GameObject[] items;
    public GameObject bullet;

    public Rigidbody2D rb;

    public Transform firePoint;

    public int health = 3;

    public bool shouldShoot;
    public bool shouldChase;
    public bool shouldWander;
    public bool isWandering;

    public float fireRate = .5f;
    public float WalkTime;
    public float waitTime;
    public float playerRange = 10f;
    public float speed;

    private float shotCounter;
    private float wanderCounter;
    private float WaitCounter;
    private int WalkDir;
    private int drop;
    private Rigidbody2D myRB;
    // Start is called before the first frame update
    void Start()
    {
        WaitCounter = waitTime;
        wanderCounter = WalkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldChase)
        {
            if (Vector3.Distance(transform.position, Player.instance.transform.position) < playerRange)
            {
                Vector3 playerDir = Player.instance.transform.position - transform.position;

                rb.velocity = playerDir.normalized * speed;

                if (shouldShoot)
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        shotCounter = fireRate;
                        if (!AudioController.instance.enemyShot.isPlaying)
                        {
                            AudioController.instance.enemyShot.Play();
                        }
                    }
                }
            }
            else if (shouldWander == true)
            {
                Wander();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, Player.instance.transform.position) < playerRange)
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shotCounter = fireRate;
                    if (!AudioController.instance.enemyShot.isPlaying)
                    {
                        AudioController.instance.enemyShot.Play();
                    }
                }
            }
            else
            {
                Wander();
            }
        }
    }
    public void Wander()
    {
        if (isWandering)
        {
            wanderCounter -= Time.deltaTime;

            if (wanderCounter < 0)
            {
                isWandering = false;
                WaitCounter = waitTime;
            }
            switch (WalkDir)
            {
                case 0:
                    rb.velocity = new Vector2(0, speed);
                    break;
                case 1:
                    rb.velocity = new Vector2(speed, 0);
                    break;
                case 2:
                    rb.velocity = new Vector2(0, -speed);
                    break;
                case 3:
                    rb.velocity = new Vector2(-speed, 0);
                    break;
            }
        }
        else
        {
            WaitCounter -= Time.deltaTime;

            rb.velocity = Vector2.zero;
            if (WaitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }
    public void ChooseDirection()
    {
        WalkDir = Random.Range(0, 4);
        isWandering = true;
        wanderCounter = WalkTime;
        if (!AudioController.instance.enemyAmbient.isPlaying)
        {
            AudioController.instance.enemyAmbient.Play();
        }

    }

    public void TakeDamage()
    {
        health--;
        if (!AudioController.instance.enemyHurt.isPlaying)
        {
            AudioController.instance.enemyHurt.Play();
        }
        if (health <= 0)
        {
            if (!AudioController.instance.enemyDeath.isPlaying)
            {
                AudioController.instance.enemyDeath.Play();
            }
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Player.instance.UpdateKillsUI();
            drop = Random.Range(1, 15);
            if (drop % 2 == 0)
            {
                Instantiate(items[Random.Range(0, items.Length)], transform.position, transform.rotation);
            }
        }
    }
}
