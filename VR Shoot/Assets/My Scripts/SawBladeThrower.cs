using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeThrower : MonoBehaviour
{
    public bool shoot;

    public float shootError;

    public GameObject blade;
    
    public int lineIterations;
    public GameObject target;
    public GameObject spawnedBlade;

    public float throwerAngle;

    private float time;

    public float untilTime;

    private Vector3 distError;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x, 0f, target.transform.position.z) - new Vector3(transform.position.x, 0f, transform.position.z) - distError);

        transform.rotation = Quaternion.Euler(Vector3.left * throwerAngle + transform.rotation.eulerAngles);

        if(shoot)
        {
            //ShootBlade(throwerAngle);
            MyFunctions.Shoot("Sawblade", throwerAngle, distError, transform, target.transform, blade, "ShootSound");
            shoot = false;
        }

        if(time > untilTime && !GameManager.gameOver && GameManager.gameStart)// && PhysicsButton.gameStart == true)
        {
            time = 0;
            //ShootBlade(throwerAngle);
            //MyFunctions.Shoot("Sawblade", throwerAngle, distError, transform, target.transform, blade, "ShootSound");
            StartCoroutine(Shoot());
        }
        time += Time.deltaTime;
        
   
    }
    IEnumerator Shoot()
    {
        MyFunctions.Shoot("Sawblade", throwerAngle, distError, transform, target.transform, blade, "ShootSound");
        yield return new WaitForSeconds(1);
        throwerAngle = Random.Range(30, 85);
        MyFunctions.FindDistError(shootError);
    }
    
}
