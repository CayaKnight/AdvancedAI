using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    public GameObject explosion;
    public GameObject bullet;

    public Animator BossMove;

    public Rigidbody2D rb;

    public Transform firePoint;

    public bool isWandering;

    public float fireRate = .5f;
    public float playerRange = 10f;
    public float speed;
    public float WalkTime;
    public float waitTime;

    public bool isFlipped = false;
    private float shotCounter;
    public int health = 3;

    private float wanderCounter;
    private float WaitCounter;
    private int WalkDir;

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
        Vector3 playerDir = Player.instance.transform.position - transform.position;
        Wander();
        if (rb.velocity != Vector2.zero)
        {
            BossMove.SetBool("IsMoving", true);
        }
        else
        {
            BossMove.SetBool("IsMoving", false);
        }
        Shoot();
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > Player.instance.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < Player.instance.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Shoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = fireRate;
        }
    }

    public void TakeDamage()
    {
        health--;
        if (!AudioController.instance.bossHurt.isPlaying)
        {
            AudioController.instance.bossHurt.Play();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!AudioController.instance.bossDeath.isPlaying)
        {
            AudioController.instance.bossDeath.Play();
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
    }
}
