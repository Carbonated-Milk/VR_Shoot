using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLazer : MonoBehaviour
{
    public bool fire;
    public bool fireOn;
    public GameObject laserPrefab;
    public GameObject firePoint;

    private GameObject spawnedLaser;
    void Start()
    {
        spawnedLaser = Instantiate (laserPrefab, firePoint.transform) as GameObject;
        DisableLaser();
        fireOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fire && fireOn == false)
        {
            EnableLaser();
        }
        else if(fire)
        {
           UpdateLaser();
        }

        if(!fire)
        {
            DisableLaser();
        }
    }

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
        fireOn = true;
    }

    void UpdateLaser()
    {
        if(firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }
    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
        fireOn = false;
    }
}
