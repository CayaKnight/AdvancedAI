using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int AAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.instance.ammo += AAmount;
            Player.instance.UpdateAmmoUI();

            if (!AudioController.instance.ammoPickUp.isPlaying)
            {
                AudioController.instance.ammoPickUp.Play();
            }

            Destroy(gameObject);
        }
    }
}
