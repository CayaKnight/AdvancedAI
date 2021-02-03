using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public Transform doorModel;
    public GameObject colObj;

    public float openSpeed;
    private bool shouldOpen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldOpen && doorModel.position.z != 1.7f)
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, doorModel.position.y, 1.7f), openSpeed * Time.deltaTime);
            if (doorModel.position.z == 1.7f)
            {
                colObj.SetActive(false);

            }


        }
        else if (!shouldOpen && doorModel.position.z != -1.5f)
        {

            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, doorModel.position.y, -1.5f), openSpeed * Time.deltaTime);
            if (doorModel.position.z == -1.5f)
            {
                colObj.SetActive(true);

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Player.instance.killCount == 30)
            {
                shouldOpen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shouldOpen = false;

        }
    }
}
