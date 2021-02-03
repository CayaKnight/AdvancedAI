using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemy;
    public Transform spawnMarker1, spawnMarker2;
    public float xPos, yPos;
    public int enemyCount, maxEnemies;

    int randomEnemy;
    // Start is called before the first frame update
    void Start()
    {


    }


    IEnumerator Spawn()
    {
        while (enemyCount < maxEnemies)
        {
            xPos = Random.Range(spawnMarker1.position.x, spawnMarker2.position.x);
            yPos = Random.Range(spawnMarker1.position.y, spawnMarker2.position.y);
            Instantiate(enemy[(Random.Range(0, enemy.Length))], new Vector3(xPos, yPos, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;

        }

    }

    private void OnTriggerEnter2D(Collider2D triggerPoint)
    {
        if (triggerPoint.tag == "Player")
        {
            StartCoroutine(Spawn());
        }
    }
}
