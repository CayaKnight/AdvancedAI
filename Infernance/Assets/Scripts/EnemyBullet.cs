using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damageAmount;

    public float bulletSpeed = 5f;

    public Rigidbody2D rb;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Player.instance.transform.position - transform.position;

        direction.Normalize();

        direction = direction * bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.instance.TakeDamage(damageAmount);

        }
        else if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
