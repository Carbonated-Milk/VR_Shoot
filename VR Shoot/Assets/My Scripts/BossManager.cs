using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public bool start;
    public GameObject lordGigamongus;
    public GameObject gun;
    public float spawnMoney;
    bool spawned;
    public void Update()
    {
        if (GameManager.money > spawnMoney && !spawned)
        {
            spawned = true;
            SpawnLordGigamongus();
        }

        if (start && GameManager.gameStart == false)
        {
            GameManager.StartGame();
            //StartCoroutine(FindObjectOfType<AudioManager>().DefeatedBoss("BossTheme"));
        }
    }
    public void SpawnLordGigamongus()
    {
        
        for(int i = 0; i < 1; i++)
        {
            Instantiate(lordGigamongus);
            GameObject gunther = Instantiate(gun);
            gunther.transform.position = Vector3.up * 2 - Vector3.right * 2;
        }
        
        GameManager.TurnOffShoot();
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Play("BossTheme");
    }
}
