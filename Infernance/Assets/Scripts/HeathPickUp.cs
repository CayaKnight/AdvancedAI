using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPickUp : MonoBehaviour
{
    public int Healamount = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.AddHealth(Healamount);
            if (!AudioController.instance.healthPickUp.isPlaying)
            {
                AudioController.instance.healthPickUp.Play();
            }
            Destroy(gameObject);
        }

    }
}
