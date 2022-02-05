using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitShoot : MonoBehaviour
{
    public bool shoot;

    public float turnTime;
    public GameObject[] shootables;
    public GameObject target;
    public GameObject cannon;

    public float shootError;
    public GameObject spawnedFruit;

    public float cannonAngle;

    private float time;

    public float untilTime;

    private bool onTarget;

    private bool locked;
    private Vector3 distError;

    private bool turnOff;

    void Start()
    {
        distError = MyFunctions.FindDistError(shootError);
    }

    void Update()
    {
        if (GameManager.gameStart && !GameManager.gameOver && !turnOff)
        {
            if (transform.rotation != Quaternion.LookRotation(new Vector3(target.transform.position.x, 0f, target.transform.position.z) - new Vector3(transform.position.x, 0f, transform.position.z) - distError) && !onTarget && !locked)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x, 0f, target.transform.position.z) - new Vector3(transform.position.x, 0f, transform.position.z) - distError), turnTime * Time.deltaTime);
            }
            else if (cannon.transform.rotation != Quaternion.Euler(Vector3.left * cannonAngle + new Vector3(0f, cannon.transform.rotation.eulerAngles.y, cannon.transform.rotation.eulerAngles.z)) && !onTarget)
            {
                locked = true;
                cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, Quaternion.Euler(Vector3.left * cannonAngle + new Vector3(0f, cannon.transform.rotation.eulerAngles.y, cannon.transform.rotation.eulerAngles.z)), turnTime * Time.deltaTime);
            }
            else if (!onTarget)
            {
                StartCoroutine(Shoot());
                onTarget = true;
            }
        }
        else
        {
            cannon.transform.rotation = Quaternion.RotateTowards(cannon.transform.rotation, Quaternion.Euler(new Vector3(0f, cannon.transform.rotation.eulerAngles.y, cannon.transform.rotation.eulerAngles.z)), turnTime * Time.deltaTime);
        }

        if (shoot)
        {
            MyFunctions.Shoot("Fruit", cannonAngle, distError, cannon.transform, target.transform, shootables[Random.Range(0, shootables.Length)], "ShootSound");
            shoot = false;
        }

    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(.5f);
        MyFunctions.Shoot("Fruit", cannonAngle, distError, cannon.transform, target.transform, shootables[Random.Range(0, shootables.Length)], "ShootSound");

        yield return new WaitForSeconds(.5f);
        cannonAngle = Random.Range(20, 40);
        distError = MyFunctions.FindDistError(shootError);
        onTarget = false;
        locked = false;
    }

    public void TurnOn()
    {
        turnOff = false;
    }

    public void TurnOff()
    {
        turnOff = true;
    }
}
