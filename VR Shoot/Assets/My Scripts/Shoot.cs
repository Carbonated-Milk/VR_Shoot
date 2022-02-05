using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public bool kill;
    public float launchPower;
    public GameObject cannonBall;

    public Transform launchPoint;
    private GameObject killBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kill)
        {
            Launch();
            kill = false;
        }
    }

    public void Launch()
    {
        GameObject killball = Instantiate(cannonBall, launchPoint) as GameObject;
        killball.GetComponent<Rigidbody>().velocity = transform.forward * launchPower;
        killball.transform.parent = null;
    }
}
