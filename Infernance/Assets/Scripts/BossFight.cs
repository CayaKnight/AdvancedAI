using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject boss;
    public Transform spawnMarker1, spawnMarker2;
    public float xPos, yPos;
    public int enemyCount, maxEnemies;

    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;
    public float timeBeforeBoss = 6;

    public int enemiesPerWave = 5;
    private int maxWaves = 2;
    private int currentWave = 0;

    int randomEnemy;

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeBeforeSpawning);

        while (currentWave < maxWaves)
        {
            if (enemyCount <= 0)
            {
                while (enemyCount < enemiesPerWave)
                {
                    xPos = Random.Range(spawnMarker1.position.x, spawnMarker2.position.x);
                    yPos = Random.Range(spawnMarker1.position.y, spawnMarker2.position.y);
                    Instantiate(enemy[(Random.Range(0, enemy.Length))], new Vector3(xPos, yPos, 0f), Quaternion.identity);
                    yield return new WaitForSeconds(timeBetweenEnemies);
                    enemyCount += 1;
                }
            }
            enemyCount = 0;
            currentWave++;
            yield return new WaitForSeconds(timeBeforeWaves);
        }
        yield return new WaitForSeconds(timeBeforeBoss);
        Instantiate(boss, new Vector3(xPos, yPos, 0f), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D triggerPoint)
    {
        if (triggerPoint.tag == "Player")
        {
            StartCoroutine(Spawn());
        }
    }
}
