using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioSource playerFootsteps, playerShot, playerDeath, ammoPickUp, healthPickUp,
        enemyAmbient, enemyShot, enemyHurt, enemyDeath, bossHurt, bossDeath, gameOver;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAmmoPickUp()
    {
        ammoPickUp.Play();
        ammoPickUp.Stop();
    }
    public void PlayHealthPickUp()
    {
        healthPickUp.Play();
        healthPickUp.Stop();
    }
    public void PlayPlayerFootsteps()
    {
        playerFootsteps.Play();
        playerFootsteps.Stop();
    }
    public void PlayPlayerShot()
    {
        playerShot.Play();
        playerShot.Stop();
    }
    public void PlayPlayerDeath()
    {
        playerDeath.Play();
        playerDeath.Stop();
    }
    public void PlayEnemyAmbient()
    {
        enemyAmbient.Play();
        enemyAmbient.Stop();
    }
    public void PlayEnemyHurt()
    {
        enemyHurt.Play();
        enemyHurt.Stop();
    }
    public void PlayEnemyShot()
    {
        enemyShot.Play();
        enemyShot.Stop();
    }
    public void PlayEnemyDeath()
    {
        enemyDeath.Play();
        enemyDeath.Stop();
    }
    public void PlayBossHurt()
    {
        bossHurt.Play();
        bossHurt.Stop();
    }
    public void PlayBossDeath()
    {
        bossDeath.Play();
        bossDeath.Stop();
    }
    public void PlayGameOver()
    {
        gameOver.Play();
        gameOver.Stop();
    }
}
