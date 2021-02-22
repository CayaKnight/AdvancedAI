using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int OpeningDirection;
    //1 = bottom door
    //2 = top door
    //3 = left door
    //4 = right door
    private RoomTemplates templates;
    private int rand;
    private bool spawned=false;
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 1f);
        
    }

    // Update is called once per frame
    void Spawn()
    {
        if (spawned==false)
        {
            if (OpeningDirection == 1)
            {
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 2)
            {
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 3)
            {
                rand = Random.Range(0, templates.ledtRooms.Length);
                Instantiate(templates.ledtRooms[rand], transform.position, templates.ledtRooms[rand].transform.rotation);
            }
            else if (OpeningDirection == 4)
            {
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(gameObject);
        if (collision.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
